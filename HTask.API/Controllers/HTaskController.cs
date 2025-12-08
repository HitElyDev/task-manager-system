using HTask.API.Models; 
using HTask.Application.Interfaces;
using HTask.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace HTask.API.Controllers
{ 
    
    [ApiController]
    [Route("api/[controller]")] 
    public class TasksController : ControllerBase
    {
        private readonly IHTaskService _taskService;
                
        public TasksController(IHTaskService taskService)
        {
            _taskService = taskService;
        }


       

        [HttpPost("CreateTask")]
        public async Task<ActionResult<HTaskReadDto>> Create([FromBody] HTaskCreateDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            
            var task = new HTaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status, 
                CreationDate = DateTime.UtcNow 
            };

            var createdTask = await _taskService.CreateTaskAsync(task);

            
            var readDto = new HTaskReadDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                Status = createdTask.Status, 
                CreationDate = createdTask.CreationDate
            };

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, readDto);
        }
        

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<HTaskReadDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();

            
            var dtos = tasks.Select(t => new HTaskReadDto { Id = t.Id, Title = t.Title, Description=t.Description,Status=t.Status,CreationDate=t.CreationDate});

            return Ok(dtos);
        }

        
        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<HTaskReadDto>> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound(); // Manejo de Errores: 404 Not Found
            }

            
            var readDto = new HTaskReadDto {Id = task.Id, Title = task.Title, Description = task.Description, Status = task.Status, CreationDate = task.CreationDate };
            return Ok(readDto);
        }

       
        [HttpPut("UpDateBy{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HTaskUpdateDto taskDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var taskToUpdate = new HTaskItem
            {
                
                Id = id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status
            };

            
            bool success = await _taskService.UpdateTaskAsync(id, taskToUpdate);

            if (!success)
            {
               
                return NotFound(); // 404 Not Found
            }

            
            return NoContent(); // 204 No Content
        }



        [HttpDelete("DeleteBy{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _taskService.DeleteTaskAsync(id);

            if (!success)
            {
                // Tarea no encontrada para eliminar
                return NotFound(); // 404 Not Found
            }

            
            return NoContent(); // 204 No Content
        }


    }
}
