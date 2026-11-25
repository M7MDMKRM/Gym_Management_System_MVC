using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Gym.Entities;

namespace MVC_Gym.Configurations
{
    public class MemberConfigurations : GymuserConfigurations<Member>
    {
        public override void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.Property(M => M.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()"); 

            base.Configure(builder);


        }
    }
}