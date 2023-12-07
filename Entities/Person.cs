using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities;

public class Person
{
    public int Id { get; init; }
    public required Address Address { get; init; }
}

public class Address
{
    public required string City { get; init; }
}

public class PersonEntityConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .OwnsOne<Address>(p => p.Address)
            .ToJson();
    }
}