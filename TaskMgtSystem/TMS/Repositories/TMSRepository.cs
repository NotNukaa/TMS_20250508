using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TMSSystem.TaskMgtSystem.TMS.Repositories;

public class TMSRepository : ITMSRepository
{
    private readonly string _connString;

    public TMSRepository(IConfiguration configuration)
    {
        _connString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<int> CreateTaskAsync(Task task)
    {
        using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();

        var param = new DynamicParameters();
        param.Add("@Title", task.Title);
        param.Add("@Description", task.Description);
        param.Add("@DueDate", task.DueDate);
        param.Add("@Priority", task.Priority);
        param.Add("@CreatedAt", task.CreatedAt);
        param.Add("@UpdatedAt", task.UpdatedAt);
        param.Add("@CreatedBy", 110);
        // param.Add("@tId",dbType: DbType.Int32, direction: ParameterDirection.Output);

        await conn.ExecuteAsync
        ("TMS_CreateTask_20250508",
            param,
            commandType: CommandType.StoredProcedure
        );
        return task.Title.Length; 
    }

    public async Task<int> DeleteTaskAsync(int id)
    {
        using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();
        
        var param = new DynamicParameters();
        param.Add("@tId", id);

        await conn.ExecuteAsync
        (
            "TMS_DeleteTask_20250508",
            param,
            commandType: CommandType.StoredProcedure
        );
        return id;
    }

    public async Task<int> UpdateTaskAsync(Task task)
    {
        using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();
        
        var param = new DynamicParameters();
        param.Add("@tId", task.Id);
        param.Add("@Title", task.Title);
        param.Add("@Description", task.Description);
        param.Add("@DueDate", task.DueDate);
        param.Add("@Priority", task.Priority);
        param.Add("@Status", task.Status);
        param.Add("@AssignedToUserId", task.AssignedToUserId);
        param.Add("@UpdatedAt", task.UpdatedAt);
        param.Add("@CreatedBy", task.CreatedBy); 
        
        await conn.ExecuteAsync
        ("TMS_UpdateTask_20250508",
            param,
            commandType: CommandType.StoredProcedure
        );
        return task.Id; 
    }

    
    public async Task<TaskDto> GetTaskAsync(int id)
    {
        using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();

        var param = new DynamicParameters();
        param.Add("@tId", id);
        
        var result = await conn.QueryFirstOrDefaultAsync<TaskDto>(
            "TMS_GetTaskId_20250508",
            param,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
    
    public async Task<List<TaskDto>> GetAllTaskAsync()
    {
        using var conn = new SqlConnection(_connString);
        await conn.OpenAsync();
    
        var result = await conn.QueryAsync<TaskDto>(
            "TMS_GetAllTask_20250508",
            commandType: CommandType.StoredProcedure
        );
    
        return result.ToList(); // Explicitly convert to List
    }

    // public async Task<<TaskDto> GetAllTaskAsync()
    // {
    //     using var conn = new SqlConnection(_connString);
    //     await conn.OpenAsync();
    //     
    //     var result = await conn.QueryAsync<TaskDto>
    //     (
    //         "TMS_GetAllTask_20250508",
    //         commandType: CommandType.StoredProcedure
    //     );
    //     return result;
    // }

    
    
}