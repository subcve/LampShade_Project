using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUpload _fileUpload;

		public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUpload fileUpload)
		{
			_productPictureRepository = productPictureRepository;
			_productRepository = productRepository;
			_fileUpload = fileUpload;
		}

		public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var filePath = $"{product.Category.Slug}/{product.Slug}";
            var fileName = _fileUpload.Upload(command.Picture, filePath);
            var productPicture = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var filePath = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var fileName = _fileUpload.Upload(command.Picture, filePath);
            productPicture.Edit(command.ProductId,fileName,command.PictureAlt,command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succeed();

        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture =  _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succeed();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
