using ArabamComCase.Core.DTOs;

namespace ArabamComCase.Core.Models
{
    public class AdvertGetAllDto
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public List<AdvertDto> Adverts { get; set; }
    }
}
