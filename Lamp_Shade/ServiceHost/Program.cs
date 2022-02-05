using _0_Framework.Application;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// config database with bootstrappers
var conecctionString = builder.Configuration.GetConnectionString("LambShadeDB");
ShopManagementBootstrapper.Configure(builder.Services,conecctionString);
DiscountManagementBootstrapper.Configure(builder.Services, conecctionString);
InventoryManagementBootstarpper.Configure(builder.Services, conecctionString);

builder.Services.AddTransient<IFileUpload,FileUpload>();

builder.Services.AddRazorPages();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
