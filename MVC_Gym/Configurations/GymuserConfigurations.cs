using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Gym.Entities;

namespace MVC_Gym.Configurations
{
    public class GymuserConfigurations<T> : IEntityTypeConfiguration<T> where T : Gymuser
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(U => U.Name)
                .HasColumnType("varchar(50)") 
                .HasMaxLength(50);

            builder.Property(U => U.PhoneNumber)
                .HasColumnType("varchar(11)")
                .HasMaxLength(11);

            builder.HasIndex(U => U.Email).IsUnique();
            builder.HasIndex(U => U.PhoneNumber).IsUnique();

            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint($"CK_{typeof(T).Name}_PhoneNumber", "PhoneNumber LIKE '01%' AND LEN(PhoneNumber) = 11");
                TB.HasCheckConstraint($"CK_{typeof(T).Name}_Email", "Email LIKE '%@%.%'");
            });

            builder.ComplexProperty(U => U.Address);
            
        }
    }
}