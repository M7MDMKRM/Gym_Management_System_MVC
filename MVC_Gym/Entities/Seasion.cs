namespace MVC_Gym.Entities
{
    public class Seasion : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TrainerId { get; set; } 
        public Traineer? Trainer { get; set; } = default!;

    }
}
