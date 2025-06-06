using Microsoft.EntityFrameworkCore;
using BACKENDD.Models;
using BACKENDD.Data;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

// ������������ Application Insights
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
});

// ������������
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ���� ������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Health Checks � ��������� ��
builder.Services.AddHealthChecks()
    .AddNpgSql(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "postgresql",
        tags: new[] { "db", "postgresql", "ready" })
    .ForwardToPrometheus();

// ������� ����������
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddControllersWithViews();
builder.Logging.AddApplicationInsights();


var app = builder.Build();

// ������������� ��
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);
}

// �������� middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home")),
    RequestPath = "/homefiles"
});

// ������� Prometheus
app.UseHttpMetrics();
app.UseMetricServer();

// �������������
app.UseRouting();
app.UseAuthorization();

// Health Check endpoint
app.MapHealthChecks("/health");
app.MapMetrics();

// �������� ������������
app.MapControllerRoute(
    name: "showcontacts",
    pattern: "Contact/ShowContacts",
    defaults: new { controller = "Contact", action = "ShowContacts" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();