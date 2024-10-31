using AspNetCoreMVC_Sepet.Models.Context;
using AspNetCoreMVC_Sepet.Sessions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DbContext
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer("server=.;database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;"));

//Session Service
builder.Services.AddSession(x =>
{
    x.Cookie.Name = "Sepet_Session";
    x.IdleTimeout = TimeSpan.FromMinutes(1);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
