using _0_Framework.Application;
using _01_Query.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _01_Query.Query
{
	public class ArticleQuery : IArticleQuery
	{
		private readonly BlogContext _context;

		public ArticleQuery(BlogContext context)
		{
			_context = context;
		}

		public ArticleQueryViewModel GetArticleDetails(string slug)
		{
			var article = _context.Articles.Include(z => z.Category).Where(c => c.PublishDate <= DateTime.Now).Select(x => new ArticleQueryViewModel
			{
				Id = x.Id,
				CategoryId = x.CategoryId,
				Description = x.Description,
				KeyWords = x.KeyWords,
				MetaDescription = x.MetaDescription,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				PublishDate = x.PublishDate.ToFarsi(),
				ShortDescription = x.ShortDescription,
				Slug = x.Slug,
				Title = x.Title,
				CategoryName = x.Category.Name,
				CategorySlug = x.Category.Slug,
				Picture = x.Picture
			}).AsNoTracking().FirstOrDefault(c=>c.Slug == slug);

			article.KeywordList = article.KeyWords.Split(",").ToList();

			return article;
		}

		public List<ArticleQueryViewModel> GetLatestArticles()
		{
			return _context.Articles.Include(z=>z.Category).Where(c=>c.PublishDate <= DateTime.Now).Select(x=> new ArticleQueryViewModel
			{
				Id = x.Id,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				PublishDate = x.PublishDate.ToFarsi(),
				ShortDescription = x.ShortDescription,
				Title = x.Title,
				Picture = x.Picture,
				Slug = x.Slug
			}).AsNoTracking().OrderByDescending(c=>c.Id).Take(3).ToList();
		}
	}
}
