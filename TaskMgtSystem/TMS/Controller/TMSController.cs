using Microsoft.AspNetCore.Mvc;
using TaskMgtSystem.TMS.Repositories;
using TaskMgtSystem.TMS.Service;

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
}