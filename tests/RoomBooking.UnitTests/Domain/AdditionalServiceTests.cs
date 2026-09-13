using RoomBooking.Domain.Entities;
using RoomBooking.Domain.Exceptions;

namespace RoomBooking.UnitTests.Domain;

public sealed class AdditionalServiceTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesActiveService()
    {
        var service = new AdditionalService(
            "Проєктор",
            500m);

        Assert.NotEqual(Guid.Empty, service.Id);
        Assert.Equal("Проєктор", service.Name);
        Assert.Equal(500m, service.Price);
        Assert.True(service.IsActive);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsDomainException()
    {
        var action = () => new AdditionalService(
            "",
            500m);

        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal("Service name is required.", exception.Message);
    }

    [Fact]
    public void Constructor_WithInvalidPrice_ThrowsDomainException()
    {
        var action = () => new AdditionalService(
            "Проєктор",
            0m);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Rename_WithValidName_UpdatesName()
    {
        var service = new AdditionalService(
            "Проєктор",
            500m);

        service.Rename("4K Проєктор");

        Assert.Equal("4K Проєктор", service.Name);
    }

    [Fact]
    public void ChangePrice_WithValidPrice_UpdatesPrice()
    {
        var service = new AdditionalService(
            "Проєктор",
            500m);

        service.ChangePrice(600m);

        Assert.Equal(600m, service.Price);
    }

    [Fact]
    public void Deactivate_ActiveService_MakesServiceInactive()
    {
        var service = new AdditionalService(
            "Проєктор",
            500m);

        service.Deactivate();

        Assert.False(service.IsActive);
    }
}