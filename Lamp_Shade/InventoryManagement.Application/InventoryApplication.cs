using _01_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IAuthHelper _authHelper;
		public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
		{
			_inventoryRepository = inventoryRepository;
			_authHelper = authHelper;
		}
		public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);
            if (_inventoryRepository.Exists(c => c.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);
            if (_inventoryRepository.Exists(c => c.ProductId == command.ProductId && c.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationsLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationsLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();

            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);

            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            const long operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();

            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);

            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (inventory.CalculateCurrentCount()<command.Count)
                return operation.Failed("مقدار مورد نظر از موجودی انبار بیشتر است");
            var operatorId = _authHelper.GetCurrentAccountId();
            inventory.Reduce(command.Count, operatorId, command.Description, 0);
            _inventoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();

            if (command == null)
                return operation.Failed(ApplicationMessages.NullRecord);


            var operatorId = _authHelper.GetCurrentAccountId();

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrederId);
            }
            _inventoryRepository.SaveChanges();

            return operation.Succeed();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}