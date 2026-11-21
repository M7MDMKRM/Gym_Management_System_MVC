using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management_System_MCV_C48.ViewModels.SessionViewModels
{
    public class SessionListViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public string Status => DateTime.Now < StartDate ? "Upcoming" : (DateTime.Now <= EndDate ? "Ongoing" : "Completed");
    }

    public class SessionCreateViewModel
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 25, ErrorMessage = "Capacity must be between 1 and 25")]
        public int Capacity { get; set; } = 15;

        [Required(ErrorMessage = "Start date and time is required")]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(1).AddHours(10);

        [Required(ErrorMessage = "End date and time is required")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1).AddHours(11);

        [Required(ErrorMessage = "Trainer is required")]
        public int TrainerId { get; set; }

        public IEnumerable<SelectListItem>? TrainersList { get; set; }
    }

    public class SessionEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 25, ErrorMessage = "Capacity must be between 1 and 25")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Start date and time is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date and time is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Trainer is required")]
        public int TrainerId { get; set; }

        public IEnumerable<SelectListItem>? TrainersList { get; set; }
    }
}

