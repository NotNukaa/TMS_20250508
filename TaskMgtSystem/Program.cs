using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TaskMgtSystem.TMS.Controller;
using TaskMgtSystem.TMS.Repositories;
using TaskMgtSystem.TMS.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<ITMSService, TMSService>();
builder.Services.AddScoped<ITMSRepository, TMSRepository>();
builder.Services.AddControllers().AddApplicationPart(typeof(TaskController).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "TmsApi", Version = "v1"
    });
});   

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();