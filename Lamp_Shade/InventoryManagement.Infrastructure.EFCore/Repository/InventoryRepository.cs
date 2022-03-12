using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

#nullable disable

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long,Inventory> , IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
		public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
		{
			_context = context;
			_shopContext = shopContext;
			_accountContext = accountContext;
		}

		public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(c => c.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(c => new EditInventory
            {
                Id = c.Id,
                ProductId = c.ProductId,
                UnitPrice = c.UnitPrice
            }).FirstOrDefault(c => c.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationsLog(long inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(c => new { c.Id, c.Fullname }).ToList();
            var inventory = _context.Inventory.FirstOrDefault(c => c.Id == inventoryId);
            var operations = inventory.Operations.Select(c => new InventoryOperationViewModel
            {
                Id = c.Id,
                Count = c.Count,
                CurrentCount = c.CurrentCount,
                Description = c.Description,
                OperationDate = c.OperationDate.ToFarsi(),
                OperatorId = c.OperatorId,
                Operation = c.Operation,
                OrderId = c.OperatorId
            }).OrderByDescending(c => c.Id).ToList();

            operations.ForEach(c => c.Operator = accounts.FirstOrDefault(z => z.Id == c.OrderId).Fullname);

            return operations;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(c => new { c.Id, c.Name }).ToList();
            var query = _context.Inventory.Select(c => new InventoryViewModel
            {
                Id = c.Id,
                UnitPrice = c.UnitPrice,
                InStock = c.InStock,
                ProductId = c.ProductId,
                CreationDate = c.CreationDate.ToFarsi(),
                CurrentCount = c.CalculateCurrentCount()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(c => c.ProductId == searchModel.ProductId);
            if (searchModel.InStock)
                query = query.Where(c => !c.InStock);

            var inventory = query.OrderByDescending(c => c.Id).ToList();

            inventory.ForEach(item =>
            {
                item.Product = products.FirstOrDefault(c => c.Id == item.ProductId)?.Name;
            });

            return inventory;
        }
    }
}
