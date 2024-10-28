using Showcase.Properties;
using Showcase.Services;
using Showcase.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showcase.Data;
using Showcase.Areas.Identity.Data;
using System.Configuration;
using Showcase.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShowcaseContextConnection") ?? throw new InvalidOperationException("Connection string 'ShowcaseContextConnection' not found.");
var connectionStringGame = builder.Configuration.GetConnectionString("GameDatabase") ?? throw new InvalidOperationException("Connection string 'ShowcaseContextConnection' not found.");



builder.Services.AddDbContext<ShowcaseContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<GameDbContext>(options =>
        options.UseSqlite(connectionStringGame));

builder.Services.AddDefaultIdentity<ShowcaseUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShowcaseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<GameManager>();
builder.Services.AddRazorPages();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax; // Minder streng in lokale omgeving
    options.Secure = CookieSecurePolicy.None; // Zorgt ervoor dat de cookies ook werken zonder HTTPS
});



builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 14;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "__Host-SecureCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.Path = "/";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

using (var scope = app.Services.CreateScope()) 
{ 
    var dbContext = scope.ServiceProvider.GetRequiredService<ShowcaseContext>(); 
    var gameDbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
    gameDbContext.Database.Migrate();
    dbContext.Database.Migrate(); 
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}



app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy",
                                "default-src 'self';" +
                                "script-src-elem 'self' 'unsafe-inline' https://code.jquery.com https://ajax.aspnetcdn.com https://www.google.com https://www.gstatic.com https://cdnjs.cloudflare.com;" +
                                "style-src 'self' 'unsafe-inline';" +
                                "img-src 'self' data:;" +
                                "frame-src 'self' https://www.google.com;" +
                                "font-src 'self';");
    await next();
});

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = false,
    DefaultContentType = "application/octet-stream"
});

app.UseCookiePolicy();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
    context.Response.Headers.Add("Pragma", "no-cache");
    context.Response.Headers.Add("Expires", "0");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=15724800; includeSubDomains");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

    await next();
});


app.UseMiddleware<ContentTypeMiddleware>();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Profile}/{id?}");

app.MapHub<GameHub>("/gameHub");
app.MapRazorPages();

app.Run();
