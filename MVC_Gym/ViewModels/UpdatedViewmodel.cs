using MVC_Gym.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVC_Gym.ViewModels
{
    public class UpdatedViewmodel
    {
        [Display(Name = "Plans")]
        public string PlanName { get; set; } = string.Empty;
        [Required (ErrorMessage = "Description is required")]
        [StringLength(200,MinimumLength =5,ErrorMessage ="Description Must be between 5 and 200 ")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "DurationDays is required")]
        [Display(Name = "Duration(Days)")]
        [Range(1 , 365 , ErrorMessage = "DurationDays Must be between 1 and 365 ")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price(EGP)")]
        [Range(0.01, 10000, ErrorMessage = "Price Must be greatear 0 ")]

        public decimal Price { get; set; }
    }
}
