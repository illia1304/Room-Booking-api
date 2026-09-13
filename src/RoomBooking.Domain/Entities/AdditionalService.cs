using RoomBooking.Domain.Exceptions;
namespace RoomBooking.Domain.Entities;

public sealed class AdditionalService
{
    private const int MaximumNameLength = 50;
    
    private AdditionalService()
    {
        
    }

    public AdditionalService(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
        Price = ValidatePrice(price);
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public void Rename(string name)
    {
        Name = ValidateName(name);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangePrice(decimal price)
    {
        Price = ValidatePrice(price);
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
            throw new DomainException("Service name is required.");
        }

        var trimmedName = name.Trim();

        if (trimmedName.Length > MaximumNameLength)
        {
            throw new DomainException(
                $"Service name cannot exceed {MaximumNameLength} characters.");
        }

        return trimmedName;
    }

    private static decimal ValidatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new DomainException("Price must be greater than zero.");
        }
        return price;
    }
}