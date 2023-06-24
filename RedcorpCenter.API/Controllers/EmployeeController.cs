using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using Newtonsoft.Json;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

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
        public IConfiguration _configuration;
        public EmployeeController(IEmployeeInfraestructure employeeInfraestructure, IEmployeeDomain employeedomain, IMapper mapper, IConfiguration configuration)
        {
            _employeeInfraestructure = employeeInfraestructure;
            _employeeDomain = employeedomain;
            _mapper = mapper;
            _configuration = configuration;

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

            

            var employeeResponse = _mapper.Map<Employee, EmployeeResponse>(employee);

            return employeeResponse;

        }

        // POST: api/Tutorial
        [HttpPost]
        public void Post([FromBody] EmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                

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
        public void Put(int id, [FromBody] EmployeeRequest employeeRequest)
        {
            

            _employeeDomain.update(id, employeeRequest.Name, employeeRequest.last_name, employeeRequest.email, employeeRequest.area, employeeRequest.cargo);
        }

        // DELETE: api/Tutorial/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeDomain.delete(id);
        }

        [HttpPost]
        [Route("login")]
        public dynamic Login([FromBody] EmployeeRequestLogin requestLogin)
        {


            string email = requestLogin.email;
            string password = requestLogin.password; 

            Employee employee = _employeeDomain.LogIn(email, password);
               
            if(employee == null)
            {
                return new
                {
                    access = false,
                    message = "Credenciales incorrectas",
                    result = "",
                    id_employee = 0
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("name",employee.Name),
                new Claim("email",employee.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    signingCredentials:signIn
                );

            return new
            {
                access = true,
                message = "Operación exitosa",
                result = new JwtSecurityTokenHandler().WriteToken(token),
                employee_id= employee.Id
            };


        }

        [HttpPost]
        [Route("Signup")]
        public IActionResult Signup([FromBody] EmployeeRequest employeesignup)
        {
            var employee = _mapper.Map<EmployeeRequest,Employee>(employeesignup);
            var id = _employeeDomain.Signup(employee);

            if (id > 0)
            {
                return Ok(id.ToString());
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
