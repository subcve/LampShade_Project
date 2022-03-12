using _01_Framework.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Service;

namespace ShopManagement.Application
{
	public class OrderApplication : IOrderApplication
	{
		private readonly IAuthHelper _authHelper;
		private readonly IOrderRepository _orderRepository;
		private readonly IShopInventoryAcl _shopInventoryAcl;
		public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl shopInventoryAcl)
		{
			_orderRepository = orderRepository;
			_authHelper = authHelper;
			_shopInventoryAcl = shopInventoryAcl;
		}
		public long PlaceOrder(Cart cart)
		{
			var accountId = _authHelper.GetCurrentAccountId();
			var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount, cart.PaymentMethod);
			foreach (var item in cart.Items)
			{
				var orderItem = new OrderItem(item.Id, item.UnitPrice, item.Count, item.DiscountRate);
				order.AddItem(orderItem);
			}
			_orderRepository.Create(order);
			_orderRepository.SaveChanges();
			return order.Id;
		}
		public string PaymentSucceed(long orderId, long refId)
		{
			var order = _orderRepository.Get(orderId);
			order.PaymentSucceed(refId);
			var issueTrackingNo = CodeGenerator.Generate("S");
			order.SetIssueTrackingNo(issueTrackingNo);
			if (!_shopInventoryAcl.ReduceFromInventory(order.OrderItems))
			{
				return "";
			}
			_orderRepository.SaveChanges();
			return issueTrackingNo;


		}

		public double GetAmountBy(long id)
		{
			return _orderRepository.GetAmountBy(id);
		}

		public List<OrderViewModel> Search(OrderSearchModel searchModel)
		{
			return _orderRepository.Search(searchModel);
		}

		public void Cancel(long id)
		{
			var order = _orderRepository.Get(id);
			order.Cancel();
			_orderRepository.SaveChanges();
		}

		public List<OrderItemViewModel> GetItems(long orderId)
		{
			return _orderRepository.GetItems(orderId);
		}
	}
}
