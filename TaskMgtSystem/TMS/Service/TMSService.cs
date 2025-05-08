using TaskMgtSystem.TMS.Repositories;
using Task = TaskMgtSystem.TMS.Repositories.Task;
using TaskStatus = TaskMgtSystem.TMS.Repositories.TaskStatus;

namespace TaskMgtSystem.TMS.Service;

public class TMSService : ITMSService
{
    private readonly ITMSRepository _repository;
    private readonly ILogger<TMSService> _logger; 
    
    public TMSService(ITMSRepository repository, ILogger<TMSService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ApiResponse> CreateTaskAsync(TaskCreateRequest request)
    {
        var logUuid = Guid.NewGuid();
        var curDateTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");;
        try
        {
            var task = new Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = TaskStatus.ToDo,
                AssignedToUserId = request.AssignedToUserId,
                CreatedAt = DateTime.UtcNow
            };
            
            var CreatedTaskId = await _repository.CreateTaskAsync(task);

            if (CreatedTaskId <= 0)
            {
                return ErrorCodeResponse("500", "Error creating task", curDateTime + " " + logUuid);
            }
            else
            {
                return SuccessResponse("200", "Task created successfully", CreatedTaskId, curDateTime + " " + logUuid);
            }
            
        }
        catch (Exception ex)
        {
            WriteLog("[Error]" + curDateTime + " " + logUuid + "Api Response");
            throw ex;
        }
    }

    public void WriteLog(string logData)
    {
        _logger.LogInformation(logData);
    }

    private ApiResponse ErrorCodeResponse(string code, string message, string logData)
    {
        WriteLog("[Error]" + logData + "Api Response");
        return new ApiResponse
        {
            Code = code,
            Message = message,
            Data = null
        };
    }

    private ApiResponse SuccessResponse(string code, string message, object data, string logData)
    {
        WriteLog("[Success]" + logData + "Api Response");
        return new ApiResponse
        {
            Code = code,
            Message = message,
            Data = data
        };
    }
}