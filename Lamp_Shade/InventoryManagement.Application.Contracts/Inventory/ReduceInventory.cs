namespace InventoryManagement.Application.Contracts.Inventory
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
        public long OrederId { get; set; }
		public ReduceInventory()
		{

		}

		public ReduceInventory(long productId, long count, string description, long orederId)
		{
			ProductId = productId;
			Count = count;
			Description = description;
			OrederId = orederId;
		}
	}
}
