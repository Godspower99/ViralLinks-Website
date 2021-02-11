using System;
using ViralLinks.Models;

namespace ViralLinks.InternalServices
{
    public interface IUrlGenerator
    {
        Uri GetPageUrl(PaginationFilter filter, string route);
    }
}