using Microsoft.AspNetCore.Mvc;
using RedcorpCenter.API.Request;
using RedcorpCenter.API.Response;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure.Models;
using RedcorpCenter.Infraestructure;
using Task = RedcorpCenter.Infraestructure.Models.Task;
using RedcorpCenter.API.Filter;

namespace RedcorpCenter.API.Controllers
{
    [Authorize("user,admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskInfraestructure _taskInfraestructure;
        private ITaskDomain _taskDomain;

        public TaskController(ITaskInfraestructure taskInfraestructure, ITaskDomain taskDomain)
        {
            _taskInfraestructure= taskInfraestructure;
            _taskDomain= taskDomain;
        }
        
                
        [HttpGet]
public IActionResult Get()
{
    try
    {
        return Ok(_taskInfraestructure.GetAll());
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al obtener todas las tareas: {ex.Message}");
        return StatusCode(500);
    }
}

[HttpGet("{TaskId}")]
public IActionResult GetTaskById(int id)
{
    try
    {
        Task task = _taskInfraestructure.GetById(id);

        if (task == null)
            return NotFound();

        TaskResponse taskResponse = new TaskResponse()
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            StartDate = task.InitialDate,
            EndDate = task.FinalDate,
            IsCompleted = task.IsCompleted,
        };

        return Ok(taskResponse);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al obtener la tarea con ID {id}: {ex.Message}");
        return StatusCode(500);
    }
}

[HttpPost]
public async Task<IActionResult> PostAsync([FromBody] TaskRequest value)
{
    try
    {
        if(ModelState.IsValid)
        {
            Task task = new Task()
            {
                Name = value.Name,
                Description = value.Description,
                InitialDate = DateTime.Parse(value.StartDate),
                FinalDate = DateTime.Parse(value.FinalDate),
                IsCompleted = value.IsCompleted,
            };

            var result = await _taskDomain.SaveAsync(task);

            return result ? StatusCode(201) : StatusCode(500);
        }
        else
        {
            return StatusCode(400);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear la tarea: {ex.Message}");
        return StatusCode(500);
    }
}

[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] TaskRequest value)
{
    try
    {
        if (ModelState.IsValid)
        {
            Task task = new Task()
            {
                Name = value.Name,
                Description = value.Description,
                InitialDate = DateTime.Parse(value.StartDate),
                FinalDate = DateTime.Parse(value.FinalDate),
                IsCompleted = value.IsCompleted,
            };
            _taskDomain.update(id, task);
            return Ok();
        }
        else
        {
            return StatusCode(400);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al actualizar la tarea con ID {id}: {ex.Message}");
        return StatusCode(500);
    }
}

[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    try
    {
        _taskDomain.delete(id);
        return Ok();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al eliminar la tarea con ID {id}: {ex.Message}");
        return StatusCode(500);
    }
}

    }
}

