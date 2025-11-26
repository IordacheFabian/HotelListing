using API.Data;
using API.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        public CountriesController(HotelListingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if(country == null) return NotFound();

            return country;
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCountry(int id, Country country)
        {
            if( id != country.Id) return BadRequest("Invalid Country Id");

            _context.Entry(country).State = EntityState.Modified;

            if(country == null) return NotFound();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if(country == null) return NotFound();

            _context.Countries.Remove(country);
            
            await _context.SaveChangesAsync();

            return NoContent();
        
        }
    }
}
