﻿using Microsoft.AspNetCore.Mvc;
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
        public List<Task> Get()
        {
            return _taskInfraestructure.GetAll();
        }
        
                
        [HttpGet("{TaskId}")]
        public TaskResponse GetTaskById(int id)
        {
            Task task = _taskInfraestructure.GetById(id);
            
            TaskResponse taskResponse = new TaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.InitialDate,
                EndDate = task.FinalDate,
                IsCompleted = task.IsCompleted,
            };
            
            return taskResponse;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TaskRequest value)
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

                return result ? StatusCode(201):StatusCode(500);

            }
            else
            {
                return StatusCode(400);
            }
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TaskRequest value)
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
            }
            else
            {
                StatusCode(400);
            }
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _taskDomain.delete(id);
        }
    }
}

