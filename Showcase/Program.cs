using Showcase.Properties;
using Showcase.Services;
using Showcase.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showcase.Data;
using Showcase.Areas.Identity.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShowcaseContextConnection") ?? throw new InvalidOperationException("Connection string 'ShowcaseContextConnection' not found.");
var connectionStringGame = builder.Configuration.GetConnectionString("GameDatabase") ?? throw new InvalidOperationException("Connection string 'ShowcaseContextConnection' not found.");


builder.Services.AddDbContext<ShowcaseContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<GameDbContext>(options =>
        options.UseSqlServer(connectionStringGame));

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
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contact}/{action=Profile}/{id?}");



app.MapHub<GameHub>("/gameHub");
app.MapRazorPages();

app.Run();
