#pragma checksum "/home/exploit90/Desktop/ViralLinks/Views/Account/ForgotPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dada176d030d5e344517d65cf284e3a315528b90"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ForgotPassword), @"mvc.1.0.view", @"/Views/Account/ForgotPassword.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/exploit90/Desktop/ViralLinks/Views/_ViewImports.cshtml"
using ViralLinks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/exploit90/Desktop/ViralLinks/Views/_ViewImports.cshtml"
using ViralLinks.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/exploit90/Desktop/ViralLinks/Views/_ViewImports.cshtml"
using ViralLinks.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dada176d030d5e344517d65cf284e3a315528b90", @"/Views/Account/ForgotPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1bb1d19b31b6ee331a1c1a3d801e945521669a2e", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ForgotPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ForgotPasswordFormModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString(" Enter email"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-size:18px; font-weight:400; border: none;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ForgotPassword", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "POST", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/home/exploit90/Desktop/ViralLinks/Views/Account/ForgotPassword.cshtml"
  
    Layout = "_AccountLayout";
    ViewBag.Title = "Forgot Password";
    var request = Context.Request;
    var baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col"">
        <div class=""container"">

            <!-- Header Text -->
            <div class=""row"">
                <div class=""col-md-3""></div>
                <div class=""col-md-6 text-center"">
                    <h3 style=""font-weight: 400;font-size: 24px;"">Forgot Password</h3>
                </div>
                <div class=""col-md-3""></div>
            </div>

            <!-- Sub Text -->
            <div class=""row"">
                <div class=""col-md-3""></div>
                <div class=""col-md-6 text-center"">
                    <h3 style=""font-weight: 400;font-size: 14px;"">Please enter your email address and a link allowing you to reset your password will be sent to you. </h3>
                </div>
                <div class=""col-md-3""></div>
            </div>

            <!-- Login Form -->
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dada176d030d5e344517d65cf284e3a315528b906663", async() => {
                WriteLiteral(@"
                <!--Email-->
                <div class=""row pt-5"">
                    <div class=""col-md-3""></div>
                    <div class=""col-md-6 text-center form-control"" style=""color: #989898;margin-left: 2rem;margin-right: 2rem;
                        border-top-width: 0px;border-right-width: 0px;border-left-width: 0px;border-bottom-width: 1px;border-bottom-color: #C4C4C4;
                        padding-bottom: 3rem;font-size: 18px;"">
                        <div class=""container"">
                            <div class=""row"">
                                <div class=""col-12"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "dada176d030d5e344517d65cf284e3a315528b907570", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 43 "/home/exploit90/Desktop/ViralLinks/Views/Account/ForgotPassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Email);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("autofocus", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-3""></div>
                </div>
                
                <!-- Submit button -->
                <div class=""row pt-3"">
                    <div class=""col-md-3""></div>
                    <div class=""col-md-6 text-center"" style=""color: #989898;margin-left: 1rem;margin-right: 1rem;"">
                       <button class=""btn"" type=""submit"" style=""color: #E0E0E0; background-color: #0395FF;width: 189px;height: 42px;border-radius: 15px;font-size: 18px;font-weight: 400;""> 
                           Submit
                       </button>
                    </div>
                    <div class=""col-md-3""></div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
</div>

<style>
  input[type=""date""].valid, input[type=""date""]:focus.valid, input[type=""datetime-local""].valid, input[type=""datetime-local""]:focus.valid, input[type=""email""].valid, input[type=""email""]:focus.valid, input[type=""number""].valid, input[type=""number""]:focus.valid, input[type=""password""].valid, input[type=""password""]:focus.valid, input[type=""search-md""].valid, input[type=""search-md""]:focus.valid, input[type=""search""].valid, input[type=""search""]:focus.valid, input[type=""tel""].valid, input[type=""tel""]:focus.valid, input[type=""text""].valid, input[type=""text""]:focus.valid, input[type=""time""].valid, input[type=""time""]:focus.valid, input[type=""url""].valid, input[type=""url""]:focus.valid, textarea.md-textarea.valid, textarea.md-textarea:focus.valid {
    border-bottom: none;
    box-shadow: 0 0 0 0 transparent;
    }
    input[type=""text""]:focus:not([readonly]),  input[type=""password""]:focus:not([readonly]) {
    border: none;
    box-shadow: 0 0 0 0 transparent;
    }
</style>

<");
            WriteLiteral(@"script defer>
    var toggle = document.getElementById('password_type_toggle');
    var password_input = document.getElementById('password-input');

    toggle.onclick = function(e){
        if(password_input.type === 'password')
        {
            password_input.type = ""text"";
            toggle.firstElementChild.innerHTML = ""visibility_off"";
        }
        else if(password_input.type === 'text')
        {
            password_input.type = ""password"";
            toggle.firstElementChild.innerHTML = ""visibility"";
        }
    }
</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ForgotPasswordFormModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
