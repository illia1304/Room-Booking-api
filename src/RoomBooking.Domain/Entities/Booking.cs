using RoomBooking.Domain.Enums;
using RoomBooking.Domain.Exceptions;

namespace RoomBooking.Domain.Entities;

public sealed class Booking
{
    private Booking()
    {
    }

    public Booking(
        Guid roomId,
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        decimal roomCost,
        decimal servicesCost)
    {
        ValidateRoomId(roomId);
        ValidatePeriod(startTime, endTime);
        ValidateCosts(roomCost, servicesCost);

        Id = Guid.NewGuid();
        RoomId = roomId;
        StartTime = startTime;
        EndTime = endTime;
        RoomCost = roomCost;
        ServicesCost = servicesCost;
        TotalCost = roomCost + servicesCost;
        Status = BookingStatus.Confirmed;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Guid Id { get; private set; }

    public Guid RoomId { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset EndTime { get; private set; }

    public decimal RoomCost { get; private set; }

    public decimal ServicesCost { get; private set; }

    public decimal TotalCost { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
        {
            throw new DomainException(
                "Booking is already cancelled.");
        }

        Status = BookingStatus.Cancelled;
        CancelledAt = DateTimeOffset.UtcNow;
        UpdatedAt = CancelledAt.Value;
    }

    private static void ValidateRoomId(Guid roomId)
    {
        if (roomId == Guid.Empty)
        {
            throw new DomainException(
                "Room identifier is required.");
        }
    }

    private static void ValidatePeriod(
        DateTimeOffset startTime,
        DateTimeOffset endTime)
    {
        if (startTime >= endTime)
        {
            throw new DomainException(
                "Booking start time must be earlier than end time.");
        }
    }

    private static void ValidateCosts(
        decimal roomCost,
        decimal servicesCost)
    {
        if (roomCost <= 0)
        {
            throw new DomainException(
                "Room cost must be greater than zero.");
        }

        if (servicesCost < 0)
        {
            throw new DomainException(
                "Services cost cannot be negative.");
        }
    }
}