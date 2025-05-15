using Microsoft.EntityFrameworkCore;
using RankingServer.DBContexts;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("DB_CONNECTION is missing.");
}

builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


app.UseAuthorization();

app.MapControllers();

app.Run();