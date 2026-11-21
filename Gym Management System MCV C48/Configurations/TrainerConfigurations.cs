using Gym_Management_System_MCV_C48.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Management_System_MCV_C48.Configurations
{
    public class TrainerConfigurations : GymuserConfigurations<Trainer>
    {
        public override void Configure(EntityTypeBuilder<Trainer> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
