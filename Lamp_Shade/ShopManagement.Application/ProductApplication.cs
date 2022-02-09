using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
	public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUpload _fileUpload;
        private readonly IProductCategoryRepository _productCategoryRepository;
		public ProductApplication(IProductRepository productRepository, IFileUpload fileUpload, IProductCategoryRepository productCategoryRepository)
		{
			_productRepository = productRepository;
			_fileUpload = fileUpload;
			_productCategoryRepository = productCategoryRepository;
		}

		public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_productRepository.Exists(c => c.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var filePath = $"{categorySlug}/{slug}";
            var fileName = _fileUpload.Upload(command.Picture, filePath);
            var product = new Product(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(c => c.Name == command.Name && c.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{product.Category.Slug}/{slug}";
            var fileName = _fileUpload.Upload(command.Picture, filePath);
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug, command.CategoryId);

            _productRepository.SaveChanges();
            return operation.Succeed();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}
