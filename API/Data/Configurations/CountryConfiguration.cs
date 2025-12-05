using System;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(
            new Country
            {
                Id = 1,
                Name = "United States",
                ShortName = "US"
            },
            new Country
            {
                Id = 2,
                Name = "Canada",
                ShortName = "CA"
            },
            new Country
            {
                Id = 3,
                Name = "Mexico",
                ShortName = "MX"
            },
            new Country
            {
                Id = 4,
                Name = "United Kingdom",
                ShortName = "UK"
            },
            new Country
            {
                Id = 5,
                Name = "Germany",
                ShortName = "DE"
            },
            new Country
            {
                Id = 6,
                Name = "Romania",
                ShortName = "RO"
            }
        );
    }
}
