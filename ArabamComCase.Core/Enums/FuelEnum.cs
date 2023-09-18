using System.ComponentModel.DataAnnotations;

namespace ArabamComCase.Core.Enums
{
    public enum FuelEnum
    {
        [Display(Name = "Dizel")]
        Diesel,
        [Display(Name = "LPG & Benzin")]
        Gas,
        [Display(Name = "Benzin")]
        Benzin
    }
}
