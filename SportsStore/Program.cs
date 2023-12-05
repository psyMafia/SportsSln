using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();//method sets up the shared objects required by applications using the MVC Framework and the Razor view engine.
builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
