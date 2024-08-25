using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TM.Arquitecture.Models;
using TM.Data.EmpleadoRepository;
using TM.Data.TicketRepository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//Connection String
builder.Services.AddDbContext<TmDatabaseContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-6SDL2SS;Database=TM_database;Trusted_Connection=True;TrustServerCertificate=True;")));


//EmpleadoRepository
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

//TicketRepository
builder.Services.AddScoped<ITicketRepository, TicketRepository>();



// Configura el almacenamiento en memoria para la sesión
builder.Services.AddDistributedMemoryCache();

// Configura la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de inactividad
    options.Cookie.HttpOnly = true; // Cookie accesible solo por HTTP
    options.Cookie.IsEssential = true; // Cookie esencial para la funcionalidad
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });



// Agrega servicios para controladores y vistas
builder.Services.AddControllersWithViews();

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
app.UseAuthentication(); // Asegúrate de que esté antes de UseAuthorization
app.UseAuthorization();
app.UseSession(); // UseSession debe estar después de UseRouting


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

