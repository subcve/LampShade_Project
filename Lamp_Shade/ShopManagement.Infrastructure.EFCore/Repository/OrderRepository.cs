using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

#nullable disable

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
	{
		private readonly ShopContext _context;
		private readonly AccountContext _accountContext;
		public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
		{
			_context = context;
			_accountContext = accountContext;
		}

		public double GetAmountBy(long id)
		{
			var order = _context.Orders.Select(c => new { c.Id, c.PayAmount }).FirstOrDefault(c => c.Id == id);
			if (order != null)
				return order.PayAmount;
			return 0;
		}

		public List<OrderItemViewModel> GetItems(long orderId)
		{
			var products = _context.Products.Select(c => new { c.Id, c.Name }).ToList();
			var order = _context.Orders.FirstOrDefault(c => c.Id == orderId);
			if (order == null)
				return new List<OrderItemViewModel>();
			var items = order.OrderItems.Select(c => new OrderItemViewModel
			{
				Id = c.Id,
				ProductId = c.ProductId,
				Count = c.Count,
				DiscountRate = c.DiscountRate,
				OrderId = c.OrderId,
				UnitPrice = c.UnitPrice

			}).ToList();
			items.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId).Name);
			return items; 
		}

		public List<OrderViewModel> Search(OrderSearchModel searchModel)
		{
			var accounts = _accountContext.Accounts.Select(c => new { c.Id, c.Fullname }).ToList();
			var query = _context.Orders.Select(x => new OrderViewModel 
			{
				Id = x.Id,
				AccountId = x.AccountId,
				DiscountAmount = x.DiscountAmount,
				IsCanceled = x.IsCanceled,
				IsPaied = x.IsPaied,
				IssueTrackingNo = x.IssueTrackingNo,
				PayAmount = x.PayAmount,
				PaymentMethodId = x.PaymentMethod,
				RefId = x.RefId,
				TotalAmount = x.TotalAmount,
				CreationDate = x.CreationDate.ToFarsi()
			});

			query = query.Where(c =>c.IsCanceled == searchModel.IsCanceled);

			if (searchModel.AccountId > 0)
				query = query.Where(c => c.AccountId == searchModel.AccountId);

			var orders = query.OrderByDescending(c => c.Id).ToList();
			foreach (var order in orders)
			{
				order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
				order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
			}
			return orders;
		}
	}
}
