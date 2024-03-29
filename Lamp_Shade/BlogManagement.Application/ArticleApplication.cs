﻿using _01_Framework.Application;
using _01_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
	public class ArticleApplication : IArticleApplication
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IArticleCategoryRepository _articleCategoryRepository;
		private readonly IFileUpload _fileUpload;
		public ArticleApplication(IArticleRepository articleRepository, IFileUpload fileUpload, IArticleCategoryRepository articleCategoryRepository)
		{
			_articleRepository = articleRepository;
			_fileUpload = fileUpload;
			_articleCategoryRepository = articleCategoryRepository;
		}

		public OperationResult Create(CreateArticle command)
		{
			var operation = new OperationResult();
			if (_articleRepository.Exists(x => x.Title == command.Title))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);

			var slug = command.Slug.Slugify();
			var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
			var path = $"{categorySlug}/{slug}";
			var pictureName = _fileUpload.Upload(command.Picture, path);
			var publishDate = command.PublishDate.ToGeorgianDateTime();

			var article = new Article(command.Title, command.Description, command.ShortDescription,pictureName,command.PictureAlt,command.PictureTitle,
				publishDate,command.KeyWords,command.MetaDescription,slug,command.CanonicalAddress,command.CategoryId);

			_articleRepository.Create(article);
			_articleRepository.SaveChanges();
			return operation.Succeed();
		}

		public OperationResult Edit(EditArticle command)
		{
			var operation = new OperationResult();
			var article = _articleRepository.GetArticleWithCategory(command.Id);

			if (article == null)
				return operation.Failed(ApplicationMessages.RecordNotFound);

			if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
				return operation.Failed(ApplicationMessages.DuplicatedRecord);

			var slug = command.Slug.Slugify();
			var path = $"{article.Category.Slug}/{slug}";
			var pictureName = _fileUpload.Upload(command.Picture, path);
			var publishDate = command.PublishDate.ToGeorgianDateTime();

			article.Edit(command.Title, command.Description, command.ShortDescription, pictureName, command.PictureAlt, command.PictureTitle,
				publishDate, command.KeyWords, command.MetaDescription, slug, command.CanonicalAddress, command.CategoryId);

			_articleRepository.SaveChanges();
			return operation.Succeed();
		}

		public EditArticle GetDetails(long id)
		{
			return _articleRepository.GetDetails(id);
		}

		public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
		{
			return _articleRepository.Search(searchModel);
		}
	}
}
