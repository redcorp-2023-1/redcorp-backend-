using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using RedcorpCenter.API.Filter;

namespace RedcorpCenter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("user,admin")]
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
        public IActionResult Get()
        {
            try
            {
                var projects = _projectInfraestructure.GetAll();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(int id)
        {
            try
            {
                var project = _projectInfraestructure.GetById(id);
                if (project == null)
                    return NotFound();

                var projectResponse = new ProjectResponse()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    StartDate = project.InitialDate,
                    EndDate = project.FinalDate,
                    State = project.State,
                };

                return Ok(projectResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProjectRequest value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Project project = new Project()
                    {
                        Name = value.Name,
                        Description = value.Description,
                        InitialDate = DateTime.Parse(value.InitialDate),
                        FinalDate = DateTime.Parse(value.FinalDate),
                        State = value.State,
                    };

                    var result = await _projectDomain.SaveAsync(project);

                    return result ? StatusCode(201) : StatusCode(500);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProjectRequest value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Project project = new Project()
                    {
                        Name = value.Name,
                        Description = value.Description,
                        InitialDate = DateTime.Parse(value.InitialDate),
                        FinalDate = DateTime.Parse(value.FinalDate),
                        State = value.State,
                    };
                    _projectDomain.update(id, project);
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _projectDomain.delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
