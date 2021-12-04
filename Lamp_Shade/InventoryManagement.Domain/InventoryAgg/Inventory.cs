using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        private long CalculateCurrentCount()
        {
            var plus = Operations.Where(c => c.Opration).Sum(c => c.Count);
            var minus = Operations.Where(c => !c.Opration).Sum(c => c.Count);
            return plus - minus;
        }
        public void Increase(long count,long operatorId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(count, true, operatorId, currentCount, description, 0, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }
        public void Reduce(long count, long operatorId, string description,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(count, false, operatorId, currentCount, description, orderId, Id);
            Operations.Add(operation);
            InStock = currentCount > 0;
        }
    }

    public class InventoryOperation
    {
        public long Id { get; private set; }
        public long Count { get; private set; }
        public bool Opration { get; private set; }
        public long OperatorId { get; private set; }
        public long CurrentCount { get; private set; }
        public DateTime OperationDate { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(long count,bool opration, long operatorId, long currentCount,
            string description, long orderId, long inventoryId)
        {
            Count = count;
            Opration = opration;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;

        }
    }
}
