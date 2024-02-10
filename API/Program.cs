using API.Extensions;
using API.Middleware;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//dotnet watch  --urls=https://localhost:5001/
//dotnet dev-certs https
//dotnet watch --no-hot-reload
//dotnet add reference ../Core
//dotnet new classlib -n projectname
//dotnet sln add projectname 
//dotnet ef migrations remove -p Infrastructure -s API  
//dotnet ef migrations add InitialCreate -p Infrastructure -s
// docker-compose up --detach -- detach is for bg running

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddApplicationService(builder.Configuration);
var app = builder.Build();

//exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// use mkcert website for https://
// ng g c nav-bar --dry-run to check the logs, no changes will be made actually
// ng g c nav-bar --skip-tests
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

// try
// {
//     await context.Database.MigrateAsync();
//     await StoreContextSeed.SeedAsync(context);
// }
// catch (Exception ex)
// {
//     logger.LogError(ex.ToString(), "An error occured during migration in program.cs line 56");
// }

app.Run();
