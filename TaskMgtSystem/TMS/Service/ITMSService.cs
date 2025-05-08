using TaskMgtSystem.TMS.Repositories;

namespace TaskMgtSystem.TMS.Service;

public interface ITMSService
{
    Task<ApiResponse> CreateTaskAsync (TaskCreateRequest request);
}