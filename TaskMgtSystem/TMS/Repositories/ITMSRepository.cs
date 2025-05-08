namespace TaskMgtSystem.TMS.Repositories;

public interface ITMSRepository
{
    Task<int> CreateTaskAsync(Task task);
}