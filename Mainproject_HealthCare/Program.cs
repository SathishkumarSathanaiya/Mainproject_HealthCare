using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Mainproject_HealthCare.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Mainproject_HealthCareContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mainproject_HealthCareContext") ?? throw new InvalidOperationException("Connection string 'Mainproject_HealthCareContext' not found.")));

builder.Services.AddControllersWithViews();

// Add session and cookie authentication services
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Mainproject_HealthCare.Session";
    options.IdleTimeout = System.TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Admin/Login";

        options.LoginPath = "/Patients/Login";
        options.AccessDeniedPath = "/Patients/Login";

        options.LoginPath = "/Doctors/Login";
        options.AccessDeniedPath = "/Doctors/Login";
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session and authentication middleware
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();