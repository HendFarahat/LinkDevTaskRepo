using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student.DataAccess.Repository.IRepository;
using Student.DataAccess.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Student.Utility;
using Students.DataAccess.DbInitializer;
using Microsoft.Extensions.Logging;
using Students.DataAccess.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Transient);

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();



//builder.Services.AddLogging(builder =>
//{
//    builder.AddFile("logs/myapp-{Date}.txt"); // Change the file path as needed
//});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //configure here
    app.UseExceptionHandler("/Error");
}
var path = Directory.GetCurrentDirectory();


Log.Logger = new LoggerConfiguration()
       .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();





app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

SeedDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}