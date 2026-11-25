using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Gym.Entities;

namespace Gym_Management_System_MCV_C48.Configurations
{
    public class SessionConfigurations : IEntityTypeConfiguration<Seasion>
    {
        public void Configure(EntityTypeBuilder<Seasion> builder)
        {

            builder.Property(s => s.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");


            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("SessionCapacityCheck", "Capacity BETWEEN 1 AND 25");
                tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });

           
        }
    }
}
