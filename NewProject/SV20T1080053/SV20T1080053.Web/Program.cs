using Microsoft.AspNetCore.Authentication.Cookies;
using SV20T1080053.Web.AppCodes;

var builder = WebApplication.CreateBuilder(args);

// B? sung các service c?n dùng:
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddMvcOptions(option =>
    {
        option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //Không s? d?ng thông báo m?c ??nh cho giá tr? null                    
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.Cookie.Name = "AuthenticationCookie";
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(60);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "areaAdmin",
        areaName: "Admin",
        pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Kh?i t?o c?u hình cho ApplicationContext
ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);

app.Run();