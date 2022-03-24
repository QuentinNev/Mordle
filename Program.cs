using ConfigurationSubstitution;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mordle.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.EnableSubstitutions("%", "%");

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DEVELOPMENT context
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DevDbContext>();
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<DevDbContext>();
}
// Configure PRODUCTION context
else
{
    builder.Services.AddDbContext<ProdDbContext>();
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

WebApplication app = builder.Build();
// Configure the HTTP request pipeline and migrate the database
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    Migrate<DevDbContext>();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    Migrate<ProdDbContext>();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

void Migrate<T>() where T : DbContext
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.Migrate();
    }
}
