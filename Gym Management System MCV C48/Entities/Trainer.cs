using Gym_Management_System_MCV_C48.Entities.Enums;

namespace Gym_Management_System_MCV_C48.Entities
{
    public class Trainer : GymUser
    {
        public DateTime HireDate { get; set; }
        public Specialties Specialties { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
