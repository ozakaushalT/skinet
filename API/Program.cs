using Core.Interfaces;
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


// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//mapping services
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Add scoped means life time of http request
//Add transient once method finishes...
//Singleton till our application shuts down

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration in program.cs line 56");
}

app.Run();
