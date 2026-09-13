using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Exceptions;

namespace RoomBooking.UnitTests.Domain;

public sealed class ConferenceRoomTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesActiveRoom()
    {
        var room = new ConferenceRoom(
            "Зал A",
            50,
            2000m);

        Assert.NotEqual(Guid.Empty, room.Id);
        Assert.Equal("Зал A", room.Name);
        Assert.Equal(50, room.Capacity);
        Assert.Equal(2000m, room.BaseHourlyRate);
        Assert.True(room.IsActive);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsDomainException()
    {
        var action = () => new ConferenceRoom(
            "",
            50,
            2000m);

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("Room name is required.", exception.Message);
    }

    [Fact]
    public void Constructor_WithInvalidCapacity_ThrowsDomainException()
    {
        var action = () => new ConferenceRoom(
            "Зал A",
            0,
            2000m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void ChangeHourlyRate_WithValidRate_UpdatesRate()
    {
        var room = new ConferenceRoom(
            "Зал A",
            50,
            2000m);

        room.ChangeHourlyRate(2500m);

        Assert.Equal(2500m, room.BaseHourlyRate);
    }

    [Fact]
    public void Deactivate_ActiveRoom_MakesRoomInactive()
    {
        var room = new ConferenceRoom(
            "Зал A",
            50,
            2000m);

        room.Deactivate();

        Assert.False(room.IsActive);
    }
}