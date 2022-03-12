using _01_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication 
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        List<InventoryOperationViewModel> GetOperationsLog(long inventoryId);
    }

}
