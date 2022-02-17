using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mordle.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// D E V
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DevDbContext>(options => options.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<DevDbContext>();
}
else
{
    // P R O D
    Console.WriteLine("SWAG");
    builder.Services.AddDbContext<ProdDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ProdDbContext>();
}

// Configure password requirements
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 4;

    options.User.RequireUniqueEmail = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

app.Environment.EnvironmentName = "Production";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
