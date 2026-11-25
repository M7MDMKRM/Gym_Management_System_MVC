namespace MVC_Gym.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Membership> Memberships { get; set; } = default!;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string Status => StartDate > DateTime.Now ? "Upcoming" : EndDate < DateTime.Now ? "Active" : "Expired";

    }
}
