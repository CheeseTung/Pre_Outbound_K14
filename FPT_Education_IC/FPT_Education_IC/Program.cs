using FPT_Education_IC.Data;
using FPT_Education_IC.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/login";
    })
    .AddGoogle(options =>
    {
        IConfigurationSection configurationSection = builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = configurationSection["ClientId"];
        options.ClientSecret = configurationSection["ClientSecret"];
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("openid");
        options.ClaimActions.MapJsonKey("image", "picture");
        options.SaveTokens = true;
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as per your needs
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.Use(async (context, next) =>
//{
//    await next();

//    if (context.Response.StatusCode == 403 || context.Response.StatusCode == 400 || context.Response.StatusCode == 401 || context.Response.StatusCode == 404 || context.Response.StatusCode == 500 || context.Response.StatusCode == 503 || context.Response.StatusCode == 504)
//    {
//        context.Response.Redirect("/Account/Login");
//    }
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.Run();
