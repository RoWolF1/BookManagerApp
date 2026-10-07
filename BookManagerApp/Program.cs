using BookManagerApp.Data;
using Microsoft.EntityFrameworkCore;
using BookManagerApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1. Kết nối SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tắt hoàn toàn log câu lệnh SQL (Executed DbCommand) của EF Core trên Console
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// 2. Kích hoạt Middleware (Đặt trước khi map route)
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();