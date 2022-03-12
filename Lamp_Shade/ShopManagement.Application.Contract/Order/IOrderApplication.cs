namespace ShopManagement.Application.Contracts.Order
{
	public interface IOrderApplication
    {
        void Cancel(long id);
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        string PaymentSucceed(long orderId,long refId);
		List<OrderItemViewModel> GetItems(long orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
