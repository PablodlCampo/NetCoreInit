using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.TableDTOs
{
    public class PagesInfoDto
    {
        public PagesInfoDto(int TotalPages, int PageSize, int TotalItems, int CurrentPage)
        {
            this.TotalPages = TotalPages;
            this.PageSize = PageSize;
            this.TotalItems = TotalItems;
            this.CurrentPage = CurrentPage;
        }

        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }

        public int PageNumber => TotalPages / PageSize;

        public bool HasPreviousPage => (CurrentPage > 1);

        public bool HasNextPage => (CurrentPage < TotalPages);

        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalItems);
    }
}
