using RoomBooking.Domain.Exceptions;

namespace RoomBooking.Domain.Entities;

public sealed class ConferenceRoom
{
    private const int MaximumNameLength = 100;

    private ConferenceRoom()
    {
    }

    public ConferenceRoom(string name, int capacity, decimal baseHourlyRate)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
        Capacity = ValidateCapacity(capacity);
        BaseHourlyRate = ValidateHourlyRate(baseHourlyRate);
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public int Capacity { get; private set; }

    public decimal BaseHourlyRate { get; private set; }

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public void Rename(string name)
    {
        Name = ValidateName(name);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangeCapacity(int capacity)
    {
        Capacity = ValidateCapacity(capacity);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangeHourlyRate(decimal hourlyRate)
    {
        BaseHourlyRate = ValidateHourlyRate(hourlyRate);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Room name is required.");
        }

        var trimmedName = name.Trim();

        if (trimmedName.Length > MaximumNameLength)
        {
            throw new DomainException(
                $"Room name cannot exceed {MaximumNameLength} characters.");
        }

        return trimmedName;
    }

    private static int ValidateCapacity(int capacity)
    {
        if (capacity <= 0)
        {
            throw new DomainException(
                "Room capacity must be greater than zero.");
        }

        return capacity;
    }

    private static decimal ValidateHourlyRate(decimal hourlyRate)
    {
        if (hourlyRate <= 0)
        {
            throw new DomainException(
                "Base hourly rate must be greater than zero.");
        }

        return hourlyRate;
    }
}