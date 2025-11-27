using API.Contracts;
using API.Data;
using API.Data.Models;
using API.Models.Hotel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);
            
            if(hotel == null) return NotFound();

            return Ok(_mapper.Map<HotelDto>(hotel));
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHotel(int id, HotelDto hotelDto)
        {
            if(id != hotelDto.Id) return BadRequest("Invalid Hotel Id");

            var hotel = await _hotelsRepository.GetAsync(id);

            if(hotel == null) return NotFound("Hotel Not Found");

            _mapper.Map(hotelDto, hotel);

            await _hotelsRepository.UpdateAsync(hotel);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(CreateHotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelsRepository.AddAsync(hotel);
            await _hotelsRepository.UpdateAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if(hotel == null) return NotFound("Hotel Not Found");

            await _hotelsRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
