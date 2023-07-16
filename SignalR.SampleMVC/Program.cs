using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalR.SampleMVC.Data;
using SignalR.SampleMVC.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var connectionStringAzureSignalR = builder.Configuration.GetConnectionString("AzureSignalR") ?? throw new InvalidOperationException("Connection string 'AzureSignalR' not found.");
builder.Services.AddSignalR().AddAzureSignalR(connectionStringAzureSignalR);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

//app.MapHub<UserHub>("/hubs/userCount");
//app.MapHub<DeathlyHallowsHub>("/hubs/deathlyhallows");
//app.MapHub<HousesHub>("/hubs/houses");
//app.MapHub<ChatHub>("/hubs/chat");
app.MapHub<OrderHub>("/hubs/order");

app.Run();
