using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory 
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }

}
