using Microsoft.EntityFrameworkCore;
using TM.Arquitecture.Models;
using TM.Data.EmpleadoRepository;
using TM.Data.TicketRepository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Connection String
builder.Services.AddDbContext<TmDatabaseContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("Server=DESKTOP-PCB1OR7;Database=APDatadb;Trusted_Connection=True;TrustServerCertificate=True;")));

//EmpleadoRepository
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

//TicketRepository
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
