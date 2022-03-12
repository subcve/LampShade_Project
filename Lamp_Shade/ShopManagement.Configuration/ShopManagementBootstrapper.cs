using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using _01_Query.Contracts.Slide;
using _01_Query.Query;
using _01_Query.Contracts.ProductCategory;
using _01_Query.Contracts.Product;
using _01_Framework.Infrastructure;
using ShopManagement.Configuration.Permissions;
using _01_Query.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Service;
using ShopManagement.Infrastructure.Inventory.Acl;

namespace ShopManagement.Configuration
{
	public class ShopManagementBootstrapper
    { 
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<IShopInventoryAcl,ShopInventoryAcl>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            services.AddDbContext<ShopContext>(c 
                => c.UseSqlServer(connectionString));
        }
    }
}
