using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();

        builder.Property(u => u.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            emailBuilder.HasIndex(e => e.Address)
                .IsUnique();
        });

        builder.OwnsOne(x => x.Password, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.Hash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(512)
                .IsRequired();
        });
    }
}