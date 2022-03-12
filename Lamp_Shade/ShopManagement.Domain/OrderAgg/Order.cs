using _01_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
	public class Order : EntityBase
	{
		public long AccountId { get; private set; }
		public int PaymentMethod { get; private set; }
		public double TotalAmount { get; private set; }
		public double DiscountAmount { get; private set; }
		public bool IsPaied { get; private set; }
		public double PayAmount { get; private set; }
		public bool IsCanceled { get; private set; }
		public string? IssueTrackingNo { get; private set; }
		public long RefId { get; private set; }
		public List<OrderItem> OrderItems { get; private set; }

		public Order(long accountId, double totalAmount, double discountAmount,
			double payAmount, int paymentMethod)
		{
			AccountId = accountId;
			TotalAmount = totalAmount;
			DiscountAmount = discountAmount;
			PayAmount = payAmount;
			OrderItems = new List<OrderItem>();
			IsPaied = false;
			IsCanceled = false;
			RefId = 0;
			PaymentMethod = paymentMethod;
		}
		public void PaymentSucceed(long refId)
		{
			IsPaied = true;

			if (RefId != 0)
				RefId = refId;
		}
		public void SetIssueTrackingNo(string number)
		{
			IssueTrackingNo = number;
		}
		public void AddItem(OrderItem orderItems)
		{
			OrderItems.Add(orderItems);
		}
		public void Cancel()
		{
			IsCanceled = true;
		}

	}

}
