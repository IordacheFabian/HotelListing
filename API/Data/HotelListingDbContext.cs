using System;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class HotelListingDbContext : DbContext
{
    public HotelListingDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().HasData(
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

        modelBuilder.Entity<Hotel>().HasData(
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
                Id = 4,
                Name = "Hotel Mexico City",
                Address = "100 Reforma Ave, Mexico City",
                Rating = 4.3,
                CountryId = 3
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
