namespace Gym_Management_System_MCV_C48.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;
    }
}
