using System;
using API.Contracts;
using API.Data;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly HotelListingDbContext _context;

    public CountriesRepository(HotelListingDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Country> GetDetails(int id)
    {
        return await _context.Countries.Include(q => q.Hotels)
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}
