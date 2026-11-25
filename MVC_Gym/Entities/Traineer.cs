using MVC_Gym.Entities.Enums;

namespace MVC_Gym.Entities
{
    public class Traineer : Gymuser
    {
        public DateTime? HireDate { get; set; } = default!;
        public Specialities Speciality { get; set; } = default!;
        public ICollection<Seasion> Seasions { get; set; } = default!;


    }
}
