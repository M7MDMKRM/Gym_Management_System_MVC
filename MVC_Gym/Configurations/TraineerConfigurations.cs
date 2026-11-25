using MVC_Gym.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_Gym.Configurations
{
        public class TrainerConfigurations : GymuserConfigurations<Traineer>
        {
            public override void Configure(EntityTypeBuilder<Traineer> builder)
            {
                base.Configure(builder);

                builder.Property(t => t.CreatedAt)
                       .HasDefaultValueSql("GETDATE()");
            }
        }
    
}
