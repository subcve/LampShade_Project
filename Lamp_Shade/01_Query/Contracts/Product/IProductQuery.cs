namespace _01_Query.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetDetails(string slug);
         List<ProductQueryModel> GetLatestArrivals();
         List<ProductQueryModel> Search(string value);
    }
}