using Microsoft.AspNetCore.Authentication.Cookies;
using P013KatmanliBlog.Data;
using P013KatmanliBlog.Service.Abstract;
using P013KatmanliBlog.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "Administrator";
    x.Cookie.MaxAge = TimeSpan.FromDays(1);
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); 
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); 
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
}

app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
