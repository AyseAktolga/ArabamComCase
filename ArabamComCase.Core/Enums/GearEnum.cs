using System.ComponentModel.DataAnnotations;

namespace ArabamComCase.Core.Enums
{
    public enum GearEnum
    {
        [Display(Name = "Düz")]
        Manual,
        [Display(Name = "Yarı Otomatik")]
        SemiAutomatic,
        [Display(Name = "Otomatik")]
        Automatic
    }
}
