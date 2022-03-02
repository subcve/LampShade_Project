using _01_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Configuration.Permissions
{
	public class ShopPermissionExposer : IPermissionExposer
	{
		public Dictionary<string, List<PermissionDto>> Expose()
		{
			return new Dictionary<string, List<PermissionDto>>
			{
				{"Product",new List<PermissionDto>
					{
						new PermissionDto(ShopPermissions.CreateProduct,"CreateProduct"),
						new PermissionDto(ShopPermissions.EditProduct,"EditProduct"),
						new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
						new PermissionDto(ShopPermissions.ListProducts,"ListProducts"),
					}
				},
				{"ProductCategory",new List<PermissionDto>
					{
						new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategory"),
						new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategory"),
						new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategories"),
						new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
					}
				}
			};
		}
	}
}
