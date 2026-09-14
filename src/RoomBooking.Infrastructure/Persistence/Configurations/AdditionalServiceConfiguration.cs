using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomBooking.Domain.Entities;

namespace RoomBooking.Infrastructure.Persistence.Configurations;

public sealed class AdditionalServiceConfiguration : IEntityTypeConfiguration<AdditionalService>
{
    public void Configure(EntityTypeBuilder<AdditionalService> builder)
    {
        builder.ToTable("additional_services");
        builder.HasKey(service => service.Id);
        builder.Property(service => service.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(service => service.Name).IsUnique();
        builder.Property(service => service.Price).HasPrecision(12, 2).IsRequired();
        builder.Property(service => service.IsActive).IsRequired();
        builder.Property(service => service.CreatedAt).IsRequired();
        builder.Property(service => service.UpdatedAt).IsRequired();
    }
}