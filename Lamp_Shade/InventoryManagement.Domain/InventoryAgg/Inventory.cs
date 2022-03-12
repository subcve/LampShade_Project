using _01_Framework.Domain;

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
            CreationDate = DateTime.Now;
        }
        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }
        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(c => c.Operation).Sum(c => c.Count);
            var minus = Operations.Where(c => !c.Operation).Sum(c => c.Count);
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
}
