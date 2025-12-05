using System;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
            new Hotel
            {
                Id = 1,
                Name = "Hotel California",
                Address = "42 Sunset Blvd, Los Angeles, CA",
                Rating = 4.5,
                CountryId = 1
            },
            new Hotel
            {
                Id = 2,
                Name = "The Grand Budapest Hotel",
                Address = "1 Republic Square, Budapest",
                Rating = 4.8,
                CountryId = 6
            },
            new Hotel
            {
                Id = 3,
                Name = "The Ritz London",
                Address = "150 Piccadilly, London",
                Rating = 4.7,
                CountryId = 4
            },
            new Hotel
            {
                Id = 5,
                Name = "Fairmont Banff Springs",
                Address = "405 Spray Ave, Banff, AB",
                Rating = 4.6,
                CountryId = 2
            },
            new Hotel
            {
                Id = 6,
                Name = "Hotel Berlin Central",
                Address = "50 Alexanderplatz, Berlin",
                Rating = 4.4,
                CountryId = 5
            }
        );
    }
}
