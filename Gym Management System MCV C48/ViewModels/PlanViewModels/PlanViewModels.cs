using System.ComponentModel.DataAnnotations;

namespace Gym_Management_System_MCV_C48.ViewModels.PlanViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int ActiveMembershipsCount { get; set; }
        public bool IsLocked => ActiveMembershipsCount > 0;
    }

    public class PlanCreateViewModel
    {
        [Required(ErrorMessage = "Plan name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Plan name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration in days is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int DurationDays { get; set; } = 30;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000.0, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class PlanEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Plan name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Plan name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Duration in days is required")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000.0, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }
    }
}

