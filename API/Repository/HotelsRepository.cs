using System;
using API.Contracts;
using API.Data;
using API.Data.Models;

namespace API.Repository;

public class HotelsRepository(HotelListingDbContext context) : GenericRepository<Hotel>(context), IHotelsRepository
{
}
