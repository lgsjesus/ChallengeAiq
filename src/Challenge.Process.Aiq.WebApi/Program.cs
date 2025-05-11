using Challenge.Process.Aiq.WebApi.Abstractions;
using Challenge.Process.Aiq.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // 👈 your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // optional, only if you're using cookies or auth headers
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.RegisterConfigurations(builder.Configuration);

var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
