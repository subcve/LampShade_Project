using _0_Framework.Application;
using _01_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
	public class ArticleCategoryApplication : IArticleCategoryApplication
	{
		private readonly IArticleCategoryRepository _articleCategoryReposiroty;
		private readonly IFileUpload _fileUpload;

		public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryReposiroty, IFileUpload fileUpload)
		{
			_articleCategoryReposiroty = articleCategoryReposiroty;
			_fileUpload = fileUpload;
		}

		public OperationResult Create(CreateArticleCategory command)
		{
			var operation = new OperationResult();
			if (_articleCategoryReposiroty.Exists(c => c.Name == command.Name && c.ShowOrder == command.ShowOrder))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);
			var slug = command.Slug.Slugify();
			var pictureName = _fileUpload.Upload(command.Picture, slug);
			var articleCategory = new ArticleCategory(command.Name, pictureName, command.KeyWords, command.MetaDescription,
				command.Description, slug, command.ShowOrder, command.CanonicalAddress,command.PictureAlt,command.PictureTitle);
			_articleCategoryReposiroty.Create(articleCategory);
			_articleCategoryReposiroty.SaveChanges();
			return operation.Succeed();
		}

		public OperationResult Edit(EditArticleCategory command)
		{
			var operation = new OperationResult();
			var articleCategory = _articleCategoryReposiroty.Get(command.Id);
			if (articleCategory == null)
				return operation.Failed(ApplicationMessages.RecordNotFound);
			var slug = command.Slug.Slugify();
			var pictureName = _fileUpload.Upload(command.Picture, slug);
			if (_articleCategoryReposiroty.Exists(c => c.Name == command.Name && c.Id == command.Id && c.ShowOrder == command.ShowOrder))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);
			articleCategory.Edit(command.Name, pictureName, command.KeyWords, command.MetaDescription,
				command.Description, slug, command.ShowOrder, command.CanonicalAddress, command.PictureAlt, command.PictureTitle);
			_articleCategoryReposiroty.SaveChanges();
			return operation.Succeed();
		}

		public List<ArticleCategoryViewModel> GetArticleCategories()
		{
			return _articleCategoryReposiroty.GetArticleCategories();
		}

		public EditArticleCategory GetDetails(long id)
		{
			return _articleCategoryReposiroty.GetDetails(id);
		}

		public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
		{
			return _articleCategoryReposiroty.Search(searchModel);
		}
	}
}