using Microsoft.EntityFrameworkCore;
using ServiceB.Data;
using ServiceB.Services;
using StackExchange.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional:false);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    Console.WriteLine($"Redis configuration: {configuration}"); // Log the configuration
    if (string.IsNullOrEmpty(configuration))
    {
        throw new ArgumentNullException(nameof(configuration), "Redis connection string is null or empty.");
    }
    configuration += ",abortConnect=false"; // Append abortConnect=false
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddScoped<MessageProcessingService>();

builder.Services.AddControllers();
builder.Services.AddLogging();

var app = builder.Build();

// var messageProcessingService = app.Services.GetRequiredService<MessageProcessingService>();
//  await messageProcessingService.ProcessMessagesAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
