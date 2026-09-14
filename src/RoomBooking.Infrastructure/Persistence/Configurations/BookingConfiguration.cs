using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomBooking.Domain.Entities;

namespace RoomBooking.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{ 
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.StartTime).IsRequired();
        builder.Property(booking => booking.EndTime).IsRequired();
        builder.Property(booking => booking.RoomCost).HasPrecision(12, 2).IsRequired();
        builder.Property(booking => booking.ServicesCost).HasPrecision(12, 2).IsRequired();
        builder.Property(booking => booking.TotalCost).HasPrecision(12, 2).IsRequired();
        builder.Property(booking => booking.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(booking => booking.CreatedAt).IsRequired();
        builder.Property(booking => booking.UpdatedAt).IsRequired();
        builder.HasOne<ConferenceRoom>().WithMany().HasForeignKey(booking => booking.RoomId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(booking => new
        {
            booking.RoomId,
            booking.StartTime,
            booking.EndTime
        });
    }
}