using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infra.Models;
using RedcorpCenter.Infra;

namespace RedcorpCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectInfraestructure _projectInfraestructure;
        private IProjectDomain _projectDomain;
        
        public ProjectController(IProjectInfraestructure projectInfraestructure, IProjectDomain projectdomain)
        {
            _projectInfraestructure = projectInfraestructure;
            _projectDomain = projectdomain;
        }
        
        [HttpGet]
        public List<Project> Get()
        {
            return _projectInfraestructure.GetAll();
        }
        
        [HttpGet("{projectId}")]
        //[HttpGet("{id}", Name = "Get")]
        public ProjectResponse GetProjectById(int id)
        {
            Project project = _projectInfraestructure.GetById(id);
            
            ProjectResponse projectResponse = new ProjectResponse()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.InitialDate,
                EndDate = project.FinalDate,
                State = project.State,
            };
            
            return projectResponse;
        }
        
        [HttpPost]
        public async void Post([FromBody] ProjectRequest value)
        {
            Project project = new Project()
            {
                Name = value.Name,
                Description = value.Description,
                FinalDate = DateTime.Parse(value.FinalDate),
                State = value.State,
            };
            
            await _projectDomain.SaveAsync(project);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProjectRequest value)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project()
                {
                    Name = value.Name,
                    Description = value.Description,
                    FinalDate = DateTime.Parse(value.FinalDate),
                    State = value.State,
                };
                _projectDomain.update(id, project);
            }
            else
            {
                StatusCode(400);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _projectDomain.delete(id);
        }
    }
}


