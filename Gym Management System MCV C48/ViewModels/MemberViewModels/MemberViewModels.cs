using System.ComponentModel.DataAnnotations;
using Gym_Management_System_MCV_C48.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace Gym_Management_System_MCV_C48.ViewModels.MemberViewModels
{
    public class MemberListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Photo { get; set; }
        public string? ActivePlanName { get; set; }
        public string Status { get; set; } = "Inactive";
    }

    public class MemberCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be 11 digits and start with 010, 011, 012, or 015")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateOnly DateOfBirth { get; set; } = new DateOnly(2000, 1, 1);

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; } = Gender.Male;

        [Required(ErrorMessage = "Building number is required")]
        [Range(1, 99999, ErrorMessage = "Invalid building number")]
        public int BuildingNumber { get; set; } = 1;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        public IFormFile? PhotoFile { get; set; }
    }

    public class MemberEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be 11 digits and start with 010, 011, 012, or 015")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        public string? CurrentPhoto { get; set; }
        public IFormFile? NewPhotoFile { get; set; }
    }

    public class MemberDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Photo { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<MemberSubscriptionItemViewModel> Subscriptions { get; set; } = new();
    }

    public class MemberSubscriptionItemViewModel
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

