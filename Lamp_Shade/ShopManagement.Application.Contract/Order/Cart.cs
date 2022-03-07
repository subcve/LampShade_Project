namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
		public List<CartItem> Items { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public Cart()
        {
            Items = new List<CartItem>();
        }
        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmout;
        }
    }
}
