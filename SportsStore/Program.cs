var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();//method sets up the shared objects required by applications using the MVC Framework and the Razor view engine.
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
