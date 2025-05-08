using Microsoft.AspNetCore.Mvc;
using TMSSystem.TaskMgtSystem.TMS.Repositories;
using TMSSystem.TaskMgtSystem.TMS.Service;

namespace TaskMgtSystem.TMS.Controller;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITMSService _service;
    
    public TaskController(ITMSService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateRequest request)
    {
        var response = await _service.CreateTaskAsync(request);
        return Ok(response);
    }
    [HttpDelete] 
    public async Task<IActionResult> DeleteTask(int id)
    {
        var response = await _service.DeleteTaskAsync(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDto taskDto)
    {
        var response = await _service.UpdateTaskAsync(taskDto);
        return Ok(response);
    }
    
    [HttpGet("{id}")] 
    public async Task<IActionResult> GetTask(int id)
    {
        var response = await _service.GetTaskAsync(id);
        return Ok(response);
    }

    [HttpGet("All")]
    public async Task<ActionResult<List<ApiResponse>>> GetAllTask()  
    {
        var response = await _service.GetAllTasksAsync();
        return response;
    }
}