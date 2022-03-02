using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// config database with bootstrappers
var conecctionString = builder.Configuration.GetConnectionString("LambShadeDB");
ShopManagementBootstrapper.Configure(builder.Services,conecctionString);
DiscountManagementBootstrapper.Configure(builder.Services, conecctionString);
InventoryManagementBootstarpper.Configure(builder.Services, conecctionString);
BlogManagementBootstrapper.Configure(builder.Services, conecctionString);
CommentManagementBootsrapper.Configure(builder.Services, conecctionString);
AccountManagementBootstrapper.Configure(builder.Services, conecctionString);
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IFileUpload, FileUpload>();
builder.Services.AddTransient<IAuthHelper,AuthHelper>();
builder.Services.AddSingleton<IPasswordHasher,PasswordHasher>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorization(optioins => {
    
    optioins.AddPolicy("AdminArea", builder =>
    builder.RequireRole(new List<string> { Roles.Administration, }));
});

builder.Services.AddRazorPages()
    .AddMvcOptions(option => {
        option.Filters.Add<SecurityPageFilter>();
        })
    .AddRazorPagesOptions(options => {

        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
