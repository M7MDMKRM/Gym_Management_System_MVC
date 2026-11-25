using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Gym.Entities;

namespace Gym_Management_System_MCV_C48.Configurations
{
    public class MembershipConfigurations : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(m => m.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");


            builder.HasOne(m => m.Plan)
                   .WithMany(p => p.Memberships)
                   .HasForeignKey(m => m.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
