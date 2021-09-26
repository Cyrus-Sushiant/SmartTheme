using System.ComponentModel.DataAnnotations;

namespace SmartTheme.Core.Domain.Identity
{
    public enum Gender : byte
    {
        [Display(Name = "مشخص نشده")]
        NotSet,
        [Display(Name = "مرد")]
        Male,
        [Display(Name = "زن")]
        Female
    }
}
