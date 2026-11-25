using Microsoft.AspNetCore.Http.HttpResults;

namespace MVC_Gym.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }= default!;
    }
}
