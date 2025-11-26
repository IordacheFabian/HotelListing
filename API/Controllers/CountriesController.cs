using API.Contracts;
using API.Data;
using API.Data.Models;
using API.Models.Country;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            _mapper = mapper;
            _countriesRepository = countriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);  
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if(country == null) return NotFound();

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if( id != updateCountryDto.Id) return BadRequest("Invalid Country Id");
            
            var country = await _countriesRepository.GetAsync(id);

            if (country == null) return NotFound();

            _mapper.Map(updateCountryDto, country);

            await _countriesRepository.UpdateAsync(country);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountry)
        {
            var country = _mapper.Map<Country>(createCountry);

            await _countriesRepository.AddAsync(country);
            await _countriesRepository.UpdateAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);

            if (country == null) return NotFound();

            await _countriesRepository.DeleteAsync(id);

            await _countriesRepository.UpdateAsync(country);

            return NoContent();
        }
    }
}
