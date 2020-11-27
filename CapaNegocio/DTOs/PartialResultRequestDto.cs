using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.DTOs
{
    public class PartialResultRequestDto
    {
        public PartialResultRequestDto()
        {
            PageNumber = 1;
            SearchString = "";
        }

        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public bool AscOrDescOrder { get; set; }
        public int? ItemsPerPage { get; set; }
        public int PageNumber { get; set; }
        public Dictionary<string, string> ComboCodes { get; set; }
    }
}
