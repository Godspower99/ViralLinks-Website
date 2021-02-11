using System;
using System.Collections.Generic;
using ViralLinks.InternalServices;
using ViralLinks.Models;

namespace ViralLinks.Helpers
{
    public class PaginationHelpers<T>
    { 
        public static PagedModel<List<T>> CreatePagedResponse (List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUrlGenerator urlGenerator, string route)  
        {
            var response = new PagedModel<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages? 
                urlGenerator.GetPageUrl(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route) : null;
            response.PreviousPage = validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages ? 
                urlGenerator.GetPageUrl(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route) : null;
            response.FirstPage = urlGenerator.GetPageUrl(new PaginationFilter(1, validFilter.PageSize), route);
            response.LastPage = urlGenerator.GetPageUrl(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}