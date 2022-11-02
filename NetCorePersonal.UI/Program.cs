using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NetCorePersonal.Core.Repositories;
using NetCorePersonal.Core.Services;
using NetCorePersonal.Core.UnitOfWorks;
using NetCorePersonal.Repository.DataContext;
using NetCorePersonal.Repository.Repositories;
using NetCorePersonal.Repository.UnitOfWork;
using NetCorePersonal.Services.Mapping;
using NetCorePersonal.Services.Services;
using NetCorePersonal.UI.Helpers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), opt =>
    {
        opt.MigrationsAssembly(Assembly.GetAssembly(typeof(DatabaseContext)).GetName().Name);
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.Cookie.Name = ".NetCore6Personal.auth";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                    opt.SlidingExpiration = false;
                    opt.LoginPath = "/Account/Login";
                    opt.LogoutPath = "/Account/LogOut";
                    opt.AccessDeniedPath = "/Admin/AccessDenied";
                    opt.ReturnUrlParameter = "/Account/Login";
                });

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectServis>();

builder.Services.AddScoped<IHasher, Hasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
