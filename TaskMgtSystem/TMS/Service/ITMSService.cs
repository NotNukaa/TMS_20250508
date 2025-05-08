

using TMSSystem.TaskMgtSystem.TMS.Repositories;

namespace TMSSystem.TaskMgtSystem.TMS.Service;

public interface ITMSService
{
    Task<ApiResponse> CreateTaskAsync (TaskCreateRequest request);
    Task<ApiResponse> DeleteTaskAsync (int id);
}