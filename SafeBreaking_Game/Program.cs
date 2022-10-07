using SafeBreaking_Game.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

//DI
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => GameSession.GetGameSession(sp));


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePages();

app.UseRouting();
app.UseSession();

app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "rules",
    pattern: "{controller=Home}/{action=Rules}");
  
app.Run();
