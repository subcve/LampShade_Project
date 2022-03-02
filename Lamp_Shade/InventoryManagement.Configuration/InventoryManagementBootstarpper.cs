using _01_Framework.Infrastructure;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstarpper
    {
            public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

            services.AddDbContext<InventoryContext>(c =>
            c.UseSqlServer(connectionString));
        }
    }
}
