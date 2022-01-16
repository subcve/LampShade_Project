namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventorySearchModel 
    {
        public bool InStock { get; set; }
        public long ProductId { get; set; }
    }
}
