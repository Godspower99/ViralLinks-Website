using System.Security.Cryptography.X509Certificates;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ViralLinks.Data;
using ViralLinks.InternalServices;
using ViralLinks.Models;

namespace ViralLinks.Controllers
{
    [Route("account"),AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext dbContext;
        private ICommunicationServices comms;
        private FileSystemService fileSystemService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext,
            ICommunicationServices comms, FileSystemService fileSystemService)
        {
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ??
                throw new ArgumentNullException(nameof(signInManager));
            this.dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            this.comms = comms ??
                throw new ArgumentNullException(nameof(comms));
            this.fileSystemService = fileSystemService ??
                throw new ArgumentNullException(nameof(fileSystemService));
        }

        /// <summary>
        /// Email input validation for sign-up form
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AcceptVerbs("GET","POST")]
        [AllowAnonymous]        
        [Route("validate-email")]
        public async Task<IActionResult> ValidateEmail(string email)
        {
            if(await userManager.FindByEmailAsync(email) != null)
            {
                return Json($"{email} has already been registered");
            }
            return Json(true);
        }

        /// <summary>
        /// Email input validation for sign-up form
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AcceptVerbs("GET","POST")]
        [AllowAnonymous]        
        [Route("validate-username")]
        public async Task<IActionResult> ValidateUsername(string username)
        {
            if(await userManager.FindByNameAsync(username) != null)
            {
                return Json($"{username} has been used");
            }
            return Json(true);
        }

        [HttpGet, Route("sign-up/start"),Route("sign-up")]
        public ActionResult SignUpStart(string referral_id)
        {
            var form1Model = new SignUpFormPart1Model(referral_id);
            return View(model: form1Model);
        }

        [HttpPost, Route("sign-up/start")]
        public async Task<ActionResult> SignUpStart(SignUpFormPart1Model firstForm)
        {
            // form validation
            if(!ModelState.IsValid)
            {
                Log.Warning("Model state is invalid");
                return View(firstForm);
            }
            var firstSignUpForm = new SignUpForm(firstForm);

            // try update signup form if one exists
            if(await dbContext.SignUpForms.AnyAsync(f => f.ID == firstSignUpForm.ID))
            {
                dbContext.SignUpForms.Update(firstSignUpForm);
            }
            // create new sign up form
            else
            {
                await dbContext.SignUpForms.AddAsync(firstSignUpForm);
            }
            Log.Information("Going to sign-up page 2");
            await dbContext.SaveChangesAsync();
            return RedirectToAction(actionName: "SignUpFinish", controllerName: "Account", routeValues: firstForm);
        }

        [HttpGet, Route("sign-up/finish")]
        public ActionResult SignUpFinish(SignUpFormPart1Model firstForm)
        {
            var finalSignUpForm = new SignUpFormPart2Model(firstForm);
            return View(model:finalSignUpForm);
        }

        [HttpPost, Route("sign-up/finish")]
        public async Task<ActionResult> SignupFinish(SignUpFormPart2Model finalForm)
        {
            if(!ModelState.IsValid)
            {
                return View(model: finalForm);
            }
            var newUser = new ApplicationUser
            {
                Email = finalForm.Email,
                UserName = finalForm.Username,
            };
            var result = await userManager.CreateAsync(user: newUser, password: finalForm.Password);
            // registration failed
            if(!result.Succeeded)
            {
                Log.Warning($"Sign up failed");
                return View(finalForm);
            }

            var registereduser = await userManager.FindByEmailAsync(newUser.Email);
            await userManager.AddToRoleAsync(registereduser,Roles.Member);

            // update profile picture
            Log.Information("Uploading Profile Picture");
            await fileSystemService.UpdateProfilePicture(registereduser,finalForm.ProfilePicture);
           // await comms.SendWelcomeEmail(registereduser, Request.Host.ToUriComponent());
            Log.Information($"profile Picture :: {finalForm.ProfilePicture.FileName}");
            // TODO :: SEND ACCOUNT VERIFICATION EMAIL
            // TODO :: FIX IN AFFLIATE REGISTRATION

            // sign-in user
            var url = await this.SignInUser(registereduser);
            return Redirect(url);
        }

        [HttpGet, Route("sign-in")]
        public async Task<ActionResult> SignIn()
        {
            // re-direct already authenticated user
            if(User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if(user == null)
                {
                    await signInManager.SignOutAsync();
                    return Redirect("/account/sign-in");
                }

                // TODO :: SEND USER TO CATEGORY PAGE (HOMEPAGE)
            }
            var loginForm = new LoginFormModel();
            return View(loginForm);
        }

        [HttpPost, Route("sign-in"),ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn([Bind]LoginFormModel loginForm)
        {
            // validate form input
            if(!ModelState.IsValid)
            {
                Log.Warning("Invalid Login form");
                return View(loginForm);
            }
            // try find user by email
            var user = await userManager.FindByEmailAsync(loginForm.UsernameOrEmail);
            if(user == null)
            {
                // try find user by username
                user = await userManager.FindByNameAsync(loginForm.UsernameOrEmail);
                if(user == null)
                {
                    Log.Warning("Failed to find user for login");
                    return View(loginForm);
                }
            }
            // verifiy password
            if(!await userManager.CheckPasswordAsync(user,loginForm.Password))
            {
                Log.Warning("password validation failed");
                return View(loginForm);
            }
            var result = await SignInUser(user);
            // signin user
            if(result != string.Empty)
            {
                return Redirect(result);
            }
            Log.Warning("Complete login failure");
            return View(loginForm);
        }

        [HttpGet]
        [Route("sign-out")]
        public async Task<ActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("/home");
        }

        [HttpPost,Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordFormModel form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }

            var user = await userManager.FindByEmailAsync(form.Email);
            if(user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(action: "ResetPassword",controller:"Account",new {
                    userid = user.Id,
                    token = token
                },Request.Scheme);
                // send password reset email
                await comms.SendPasswordReset(user,link,Request.Host.ToUriComponent());
            }
            // TODO :: redirect
            return Redirect($"/account/reset-password/sent?email={user.Email}");
        }

        [HttpGet,Route("forgot-password")]
        public ActionResult ForgotPassword()
        {
            var resetForm = new ForgotPasswordFormModel();
            return View(resetForm);
        }

        [HttpPost,Route("reset-password")]
        public async Task<ActionResult> ResetPassword(PasswordResetFormModel form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }
            var user = await userManager.FindByIdAsync(form.UserID);
            if(user == null)
            {
                // TODO :: REDIRECT TO BAD REQUEST PAGE
                Log.Warning("** NO USER FOR PASSWORD RESET **");
                return Redirect("/home");
            }
            var result = await userManager.ResetPasswordAsync(user,form.Token,form.NewPassword);
            if(result.Succeeded)
            {
                // TODO :: REDIRECT TO PASSWORD RESET COMPLETED PAGE
                return Redirect("/account/reset-password/completed");
            }
            // TODO :: REDIRECT TO BAD REQUEST PAGE
            return Redirect("/home");
        }

        [HttpGet, Route("reset-password")]
        public ActionResult ResetPassword(string userid, string token)
        {
            var resetForm = new PasswordResetFormModel();
            resetForm.UserID = userid;
            resetForm.Token = token;
            return View(resetForm);
        }

        [HttpGet]
        [Route("reset-password/sent")]
        [AllowAnonymous]
        public ActionResult ResetPasswordMailSent(string email)
        {
            ViewData["email_address"] = email;
            return View();
        }

        [HttpGet]
        [Route("reset-password/completed")]
        [AllowAnonymous]
        public ActionResult ResetPasswordCompleted()
        {
            return View();
        }


        [HttpGet, Route("profile"),Authorize(AuthorizationPolicies.AuthenticatedPolicy)]
        public async Task<ActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var viewModel = new ProfileModel();
            viewModel.User = user;
            return View(model: viewModel);
        }


        [HttpGet, Route("create-post")]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpGet, Route("full-post")]
        public ActionResult FullPost()
        {
            return View();
        }

        [HttpGet, Route("pic")]
        public async Task<ActionResult> Picture(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return Ok(await this.fileSystemService.GetProfilePictureAsync(user.Id));
        }

        public async Task<ActionResult> Delete(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return Redirect("/account/sign-in");
        }

        // *************** Helper Methods ***********************
        private async Task<string> SignInUser(ApplicationUser user)
        {
            var authProps = new AuthenticationProperties();
            authProps.AllowRefresh = true;
            authProps.IsPersistent = true;
            authProps.IssuedUtc = DateTime.UtcNow;
            await signInManager.SignInAsync(user,authProps);

            // TODO :: REDIRECT USER TO CATEGORY PAGE
            return "/home";
        }
    }
}