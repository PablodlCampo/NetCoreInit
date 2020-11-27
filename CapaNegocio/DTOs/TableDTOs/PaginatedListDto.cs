using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.TableDTOs
{
    public class PaginatedListDto<T, I> where I : Enum
    {
        public PaginatedListDto(List<T> Items, int TotalPages, int TotalItems, PaginatedListRequest<I> Request)
        {
            this.Items = Items;
            this.Request = Request;

            PageInfo = new PagesInfoDto(TotalPages, Request.PageSize, TotalItems, Request.CurrentPage);
        }

        public PaginatedListRequest<I> Request { get; set; }

        public List<T> Items { get; set; }

        public PagesInfoDto PageInfo { get; set; }
    }
}
