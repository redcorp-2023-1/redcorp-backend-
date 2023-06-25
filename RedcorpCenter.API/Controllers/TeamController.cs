using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Filter;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.API.Controllers
{
    [Authorize("user,admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private ITeamInfraestructure _teamInfraestructure;
        private ITeamDomain _teamDomain;

        public TeamController(ITeamInfraestructure teamInfraestructure, ITeamDomain teamDomain)
        {
            _teamInfraestructure= teamInfraestructure;
            _teamDomain= teamDomain;
        }
        
                
        [HttpGet]
        public List<Team> Get()
        {
            return _teamInfraestructure.GetAll();
        }
        
                
        [HttpGet("{TeamId}")]
        public TeamResponse GetTeamById(int id)
        {
            Team team = _teamInfraestructure.GetById(id);
            
            TeamResponse teamResponse = new TeamResponse()
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                Id_Employee = team.Id_Employee,
                Id_Project = team.Id_Project,
                Id_Task = team.Id_Task,
            };
            
            return teamResponse;
        }
        
        [HttpPost]
        public async void Post([FromBody] TeamRequest value)
        {
            Team team = new Team()
            {
                Name = value.Name,
                Description = value.Description,
                Id_Employee = value.Id_Employee,
                Id_Project = value.Id_Project,
                Id_Task = value.Id_Task,
            };
            
            await _teamDomain.SaveAsync(team);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TeamRequest value)
        {
            if (ModelState.IsValid)
            {
                Team team = new Team()
                {
                    Name = value.Name,
                    Description = value.Description,
                    Id_Employee = value.Id_Employee,
                    Id_Project = value.Id_Project,
                    Id_Task = value.Id_Task,
                };
                _teamDomain.update(id, team);
            }
            else
            {
                StatusCode(400);
            }
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _teamDomain.delete(id);
        }
    }
}