using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Filter;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedcorpCenter.API.Controllers
{
    [Authorize("user,admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private ISectionInfraestructure _sectionInfraestructure;
        private ISectionDomain _sectionDomain;
        private IMapper _mapper;

        public SectionController(ISectionInfraestructure sectionInfraestructure, ISectionDomain sectionDomain, IMapper mapper)
        {
            _sectionInfraestructure = sectionInfraestructure;
            _sectionDomain = sectionDomain;
            _mapper = mapper;
        }



        // GET: api/<SectionController>
        [HttpGet]
        public async Task<List<SectionResponse>> GetAsync()
        {
            try
            {
                var sections = await _sectionInfraestructure.GetAllAsync();
                return _mapper.Map<List<Section>, List<SectionResponse>>(sections);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        // GET api/<SectionController>/5
        [HttpGet("{id}", Name = "GetSection")]
        public async Task<SectionResponse> GetByIdAsync(int id)
        {
            try
            {
                Section section = await _sectionInfraestructure.GetByIdAsync(id);
                
                var sectionResponse = _mapper.Map<Section, SectionResponse>(section);

                return sectionResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        // POST api/<SectionController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SectionRequest sectionRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var section = _mapper.Map<SectionRequest, Section>(sectionRequest);

                    var result = await _sectionDomain.SaveAsync(section);

                    return result ? StatusCode(201) : StatusCode(500);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            
        }

        // PUT api/<SectionController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SectionRequest sectionRequest)
        {
            try
            {
                _sectionDomain.update(id, sectionRequest.Section_Name, sectionRequest.Description);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<SectionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sectionDomain.delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}

