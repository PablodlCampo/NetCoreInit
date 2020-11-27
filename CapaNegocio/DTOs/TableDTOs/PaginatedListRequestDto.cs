using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs.TableDTOs
{
    public class PaginatedListRequest<T> where T : Enum
    {
        public int CurrentPage { get; set; }
        
        public int PageSize { get; set; }

        public List<Tuple<T, bool>> OrderList { get; set; }

        public string MultiSearch { get; set; } = "";
    }
}
