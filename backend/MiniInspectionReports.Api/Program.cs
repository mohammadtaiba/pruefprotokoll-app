using Microsoft.EntityFrameworkCore;
using MiniInspectionReports.Api.Data;
using MiniInspectionReports.Api.Repositories;
using MiniInspectionReports.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IInspectionReportRepository, InspectionReportRepository>();
builder.Services.AddScoped<IInspectionReportService, InspectionReportService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors("AngularDevClient");
app.MapControllers();

app.Run();
