using Gym_Management_System_MCV_C48.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Management_System_MCV_C48.Configurations
{
    public class GymuserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(u => u.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint($"CK_{typeof(T).Name}_Email", "Email LIKE '_%@_%._%'");
                tb.HasCheckConstraint($"CK_{typeof(T).Name}_Phone", "LEN(Phone) = 11 AND (Phone LIKE '010%' OR Phone LIKE '011%' OR Phone LIKE '012%' OR Phone LIKE '015%')");
            });

            builder.ComplexProperty(u => u.Address);
        }
    }
}
