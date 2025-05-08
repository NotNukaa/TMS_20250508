

using TMSSystem.TaskMgtSystem.TMS.Repositories;

namespace TMSSystem.TaskMgtSystem.TMS.Service;

public interface ITMSService
{
    Task<ApiResponse> CreateTaskAsync (TaskCreateRequest request);
    Task<ApiResponse> DeleteTaskAsync (int id);

    Task<ApiResponse> UpdateTaskAsync(TaskDto taskDto);
    
    Task<ApiResponse> GetTaskAsync (int id);
    
    Task<List<ApiResponse>> GetAllTasksAsync();
}