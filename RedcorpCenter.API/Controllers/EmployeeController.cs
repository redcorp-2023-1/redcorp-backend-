using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedcorpCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeInfraestructure _employeeInfraestructure;
        private IEmployeeDomain _employeeDomain;
        private IMapper _mapper;

        public EmployeeController(IEmployeeInfraestructure employeeInfraestructure, IEmployeeDomain employeedomain, IMapper mapper)
        {
            _employeeInfraestructure = employeeInfraestructure;
            _employeeDomain = employeedomain;
            _mapper = mapper;
        }


        // GET: api/Tutorial
        [HttpGet]
        public async Task<List<EmployeeResponse>> GetAsync()
        {

            var employees = await _employeeInfraestructure.GetAllAsync();

            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        }

        // GET: api/Tutorial/5
        [HttpGet("{id}", Name = "Get")]
        public EmployeeResponse Get(int id)
        {
            Employee employee = _employeeInfraestructure.GetById(id);

            //EmployeeResponse employeeResponse = new EmployeeResponse()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //};

            var employeeResponse = _mapper.Map<Employee, EmployeeResponse>(employee);

            return employeeResponse;

        }

        // POST: api/Tutorial
        [HttpPost]
        public void Post([FromBody] EmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                //Employee employee = new Employee()
                //{
                //    Name = value.Name
                //};

                var employee = _mapper.Map<EmployeeRequest, Employee>(employeeRequest);

                _employeeDomain.Save(employee);
            }
            else
            {
                StatusCode(400);
            }
        }

        // PUT: api/Tutorial/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string name, string last_name, string email)
        {
            _employeeDomain.update(id, name, last_name, email);
        }

        // DELETE: api/Tutorial/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeDomain.delete(id);
        }
    }

}
