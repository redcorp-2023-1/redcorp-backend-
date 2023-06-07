using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedcorpCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionAndEmployeeController : ControllerBase
    {
        private ISectionAndEmployeeInfraestructure _sectionAndEmployeeInfraestructure;
        private ISectionAndEmployeeDomain _sectionAndEmployeeDomain;

        public SectionAndEmployeeController(ISectionAndEmployeeInfraestructure sectionAndEmployeeInfraestructure,
            SectionAndEmployeeDomain sectionAndEmployeeDomain) 
        {
            _sectionAndEmployeeInfraestructure = sectionAndEmployeeInfraestructure;
            _sectionAndEmployeeDomain = sectionAndEmployeeDomain;
        }


        // GET: api/<SectionAndEmployeeController>
        [HttpGet]
        public async Task<List<SectionsAndEmployees>> GetAsync()
        {
            return await _sectionAndEmployeeInfraestructure.GetAllAsync();
        }

        // GET api/<SectionAndEmployeeController>/5
        [HttpGet("{id}", Name = "SectionsEmployees")]
        public SectionsAndEmployees Get(int id)
        {
            SectionsAndEmployees sectionsAndEmployees = _sectionAndEmployeeInfraestructure.GetById(id);

            return sectionsAndEmployees;
        }

        // POST api/<SectionAndEmployeeController>
        [HttpPost]
        public void Post([FromBody] SectionsAndEmployees sectionsAndEmployees)
        {
            if(ModelState.IsValid)
            {

            }
            else
            {
                StatusCode(400);
            }
        }

        // PUT api/<SectionAndEmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] int Section_Id,int Employee_Id)
        {
            _sectionAndEmployeeDomain.update(id, Section_Id, Employee_Id);  
        }

        // DELETE api/<SectionAndEmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _sectionAndEmployeeDomain.delete(id);
        }
    }
}
