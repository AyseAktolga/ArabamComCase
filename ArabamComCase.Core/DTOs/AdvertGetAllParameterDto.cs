using ArabamComCase.Core.DTOs;
using ArabamComCase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Core.Models
{
    public class AdvertGetAllParameterDto
    {
        public GearEnum GearEnum { get; set; }
        public FuelEnum FuelEnum { get; set; }
        public AdvertSorting AdvertSorting { get; set; }
        public int CategoryId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
    }
}
