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
    builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
        .AddEntityFrameworkStores<DevDbContext>();
}
// Configure PRODUCTION context
else
{
    builder.Services.AddDbContext<ProdDbContext>();
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ProdDbContext>();
}

// Define Redirection routes
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/Login";
});

// Configure password requirements
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
});

// Configure authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// Allow access only to identified users
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/game", "LoggedIn");
});

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

// Run migrations
void Migrate<T>() where T : DbContext
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.Migrate();
    }
}
