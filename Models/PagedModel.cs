using System;

namespace ViralLinks.Models
{
     public class PagedModel<TBody> 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public TBody Body { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        
        public PagedModel(TBody body, int pageNumber, int pageSize)
        {  
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Body = body;
        }
    }

    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}