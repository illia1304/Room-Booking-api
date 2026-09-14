using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomBooking.Domain.Entities;

namespace RoomBooking.Domain.Entities;

public sealed class ConferenceRoomConfiguration:IEntityTypeConfiguration<ConferenceRoom>
{
    public void Configure(EntityTypeBuilder<ConferenceRoom> builder)
    {
        builder.ToTable("conference_rooms");
        builder.HasKey(room => room.Id);
        builder.Property(room => room.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(room => room.Name).IsUnique();
        builder.Property(room => room.Capacity).IsRequired();
        builder.Property(room => room.BaseHourlyRate).HasPrecision(12, 2).IsRequired();
        builder.Property(room => room.IsActive).IsRequired();
        builder.Property(room => room.CreatedAt).IsRequired();
        builder.Property(room => room.UpdatedAt).IsRequired();
        builder.HasMany(room => room.AvailableServices)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "room_additional_services",
                right => right
                    .HasOne<AdditionalService>()
                    .WithMany()
                    .HasForeignKey("AdditionalServiceId")
                    .OnDelete(DeleteBehavior.Restrict),
                left => left
                    .HasOne<ConferenceRoom>()
                    .WithMany()
                    .HasForeignKey("RoomId")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.ToTable("room_additional_services");

                    join.HasKey(
                        "RoomId",
                        "AdditionalServiceId");
                });
    }
}