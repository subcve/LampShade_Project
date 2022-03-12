using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Service;

namespace ShopManagement.Infrastructure.Inventory.Acl
{
	public class ShopInventoryAcl : IShopInventoryAcl
	{
		private readonly IInventoryApplication _inventoryApplication;
		public ShopInventoryAcl(IInventoryApplication inventoryApplication)
		{
			_inventoryApplication = inventoryApplication;
		}

		public bool ReduceFromInventory(List<OrderItem> orderItems)
		{
			var command = new List<ReduceInventory>();
			foreach (var item in orderItems)
			{
				var result = new ReduceInventory(item.ProductId, item.Count, "خرید مشتری", item.OrderId);
				command.Add(result);
			}
			return _inventoryApplication.Reduce(command).IsSucceed;
		}
	}
}