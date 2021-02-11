using System;
using Microsoft.AspNetCore.WebUtilities;
using ViralLinks.Models;

namespace ViralLinks.InternalServices
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly string baseUrl;
        public UrlGenerator(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public Uri GetPageUrl(PaginationFilter filter, string route)
        {
            var endpointUrl = new Uri(string.Concat(baseUrl, route));
            var modifiedUrl = QueryHelpers.AddQueryString(endpointUrl.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUrl = QueryHelpers.AddQueryString(modifiedUrl, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUrl); }
    }
}