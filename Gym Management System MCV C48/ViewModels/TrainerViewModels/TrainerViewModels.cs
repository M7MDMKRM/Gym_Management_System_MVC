using System.ComponentModel.DataAnnotations;
using Gym_Management_System_MCV_C48.Entities.Enums;

namespace Gym_Management_System_MCV_C48.ViewModels.TrainerViewModels
{
    public class TrainerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime HireDate { get; set; }
        public Specialties Specialties { get; set; }
        public int SessionsCount { get; set; }
    }

    public class TrainerCreateViewModel
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
        public DateOnly DateOfBirth { get; set; } = new DateOnly(1995, 1, 1);

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; } = Gender.Male;

        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Specialty is required")]
        public Specialties Specialties { get; set; } = Specialties.GeneralFitness;

        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; } = 1;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;
    }

    public class TrainerEditViewModel
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

        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Specialty is required")]
        public Specialties Specialties { get; set; }

        [Required(ErrorMessage = "Building number is required")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;
    }

    public class TrainerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public Specialties Specialties { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<TrainerSessionItemViewModel> Sessions { get; set; } = new();
    }

    public class TrainerSessionItemViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status => DateTime.Now < StartDate ? "Upcoming" : (DateTime.Now <= EndDate ? "Ongoing" : "Completed");
    }
}

