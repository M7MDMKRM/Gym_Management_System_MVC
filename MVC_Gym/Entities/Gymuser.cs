using MVC_Gym.Entities.Enums;

namespace MVC_Gym.Entities
{
    public abstract class Gymuser : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
       
        public DateOnly DateOfBirth { get; set; } = default!;
        public Gender Gender { get; set; } = default!;

        public Address Address { get; set; } = default!;

    }
}
