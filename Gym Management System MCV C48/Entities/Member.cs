namespace Gym_Management_System_MCV_C48.Entities
{
    public class Member : GymUser
    {
        public string? Photo { get; set; }
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
