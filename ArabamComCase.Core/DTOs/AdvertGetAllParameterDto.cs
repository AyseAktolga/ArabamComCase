using ArabamComCase.Core.Enums;

namespace ArabamComCase.Core.Models
{
    public class AdvertGetAllParameterDto
    {
        public GearEnum? Gear { get; set; }
        public FuelEnum? Fuel { get; set; }
        public SortingColumn? SortingColumn { get; set; }
        public SortingOrder? SortingOrder { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int? PriceMin { get; set; }
        public int? PriceMax { get; set; }
    }
}
