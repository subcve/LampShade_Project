using _01_Framework.Application;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
	public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUpload _fileUpload;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUpload fileUpload)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var picturePath = $"{slug}";
            var pictureName = _fileUpload.Upload(command.Picture, picturePath);
            var productCategory = new ProductCategory(command.Name, command.Description,
                pictureName, command.PictureAlt, command.PictureTitle,
                command.Keywords,command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var picturePath = $"{slug}";
            var pictureName = _fileUpload.Upload(command.Picture,picturePath);
            productCategory.Edit(command.Name, command.Description, pictureName, 
                command.PictureAlt, command.PictureTitle, command.Keywords, 
                command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succeed();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
    }
}
