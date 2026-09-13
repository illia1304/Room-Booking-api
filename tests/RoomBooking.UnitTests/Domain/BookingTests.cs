using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Enums;
using RoomBooking.Domain.Exceptions;

namespace RoomBooking.UnitTests.Domain;

public sealed class BookingTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesConfirmedBooking()
    {
        var roomId = Guid.NewGuid();
        var startTime = DateTimeOffset.UtcNow.AddDays(1);
        var endTime = startTime.AddHours(2);

        var booking = new Booking(
            roomId,
            startTime,
            endTime,
            4000m,
            800m);

        Assert.NotEqual(Guid.Empty, booking.Id);
        Assert.Equal(roomId, booking.RoomId);
        Assert.Equal(startTime, booking.StartTime);
        Assert.Equal(endTime, booking.EndTime);
        Assert.Equal(4000m, booking.RoomCost);
        Assert.Equal(800m, booking.ServicesCost);
        Assert.Equal(4800m, booking.TotalCost);
        Assert.Equal(BookingStatus.Confirmed, booking.Status);
        Assert.Null(booking.CancelledAt);
    }

    [Fact]
    public void Constructor_WithEmptyRoomId_ThrowsDomainException()
    {
        var startTime = DateTimeOffset.UtcNow.AddDays(1);

        var action = () => new Booking(
            Guid.Empty,
            startTime,
            startTime.AddHours(2),
            4000m,
            0m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Constructor_WhenStartEqualsEnd_ThrowsDomainException()
    {
        var bookingTime = DateTimeOffset.UtcNow.AddDays(1);

        var action = () => new Booking(
            Guid.NewGuid(),
            bookingTime,
            bookingTime,
            4000m,
            0m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Constructor_WhenStartIsAfterEnd_ThrowsDomainException()
    {
        var endTime = DateTimeOffset.UtcNow.AddDays(1);
        var startTime = endTime.AddHours(2);

        var action = () => new Booking(
            Guid.NewGuid(),
            startTime,
            endTime,
            4000m,
            0m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Constructor_WithInvalidRoomCost_ThrowsDomainException()
    {
        var startTime = DateTimeOffset.UtcNow.AddDays(1);

        var action = () => new Booking(
            Guid.NewGuid(),
            startTime,
            startTime.AddHours(2),
            0m,
            0m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Constructor_WithNegativeServicesCost_ThrowsDomainException()
    {
        var startTime = DateTimeOffset.UtcNow.AddDays(1);

        var action = () => new Booking(
            Guid.NewGuid(),
            startTime,
            startTime.AddHours(2),
            4000m,
            -100m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Cancel_ConfirmedBooking_CancelsBooking()
    {
        var startTime = DateTimeOffset.UtcNow.AddDays(1);

        var booking = new Booking(
            Guid.NewGuid(),
            startTime,
            startTime.AddHours(2),
            4000m,
            0m);

        booking.Cancel();

        Assert.Equal(BookingStatus.Cancelled, booking.Status);
        Assert.NotNull(booking.CancelledAt);
    }

    [Fact]
    public void Cancel_CancelledBooking_ThrowsDomainException()
    {
        var startTime = DateTimeOffset.UtcNow.AddDays(1);

        var booking = new Booking(
            Guid.NewGuid(),
            startTime,
            startTime.AddHours(2),
            4000m,
            0m);

        booking.Cancel();

        var action = () => booking.Cancel();

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Booking is already cancelled.",
            exception.Message);
    }
}