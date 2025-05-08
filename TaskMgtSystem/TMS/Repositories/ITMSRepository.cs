namespace TMSSystem.TaskMgtSystem.TMS.Repositories;

public interface ITMSRepository
{
    Task<int> CreateTaskAsync(Task task);
    Task<int> DeleteTaskAsync(int id);
    
    Task<int> UpdateTaskAsync(Task task);
    
}