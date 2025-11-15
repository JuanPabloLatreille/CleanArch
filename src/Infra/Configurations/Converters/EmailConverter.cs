using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Configurations.Converters;

public class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        email => email.Address,
        address => Email.Create(address).Value)
    {
    }
}