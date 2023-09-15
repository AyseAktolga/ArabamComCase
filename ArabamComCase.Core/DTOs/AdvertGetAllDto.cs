using ArabamComCase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Core.Models
{
    public class AdvertGetAllDto
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public List<AdvertDto> Adverts { get; set; }
    }
}
