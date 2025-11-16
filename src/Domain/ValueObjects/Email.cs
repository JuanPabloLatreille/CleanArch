using Domain.Shared;

namespace Domain.ValueObjects;

public class Email
{
    private Email()
    {
        Address = string.Empty;
    }

    private Email(string address)
    {
        Address = address;
    }

    public string Address { get; private set; }

    public static Result<Email> Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return Result.Failure<Email>(EmailErrors.Empty);
        }

        if (address.Length > 255)
        {
            return Result.Failure<Email>(EmailErrors.TooLong);
        }

        if (!address.Contains('@'))
        {
            return Result.Failure<Email>(EmailErrors.InvalidFormat);
        }

        return Result.Success(new Email(address.ToLowerInvariant()));
    }

    public override string ToString() => Address;

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (Email)obj;
        return Address == other.Address;
    }

    public override int GetHashCode() => Address.GetHashCode();

    public static bool operator ==(Email? left, Email? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Email? left, Email? right) => !(left == right);
}