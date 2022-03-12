using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlogManagement.Infrastructure.EFCore.Repository
{
	public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
	{
		private readonly BlogContext _context;
		public ArticleRepository(BlogContext context) : base(context)
		{
			_context = context;
		}

		public Article GetArticleWithCategory(long id)
		{
			return _context.Articles.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
		}

		public EditArticle GetDetails(long id)
		{
			return _context.Articles.Select(x => new EditArticle 
			{
				Id = x.Id,
				CanonicalAddress = x.CanonicalAddress,
				CategoryId = x.CategoryId,
				Description = x.Description,
				KeyWords = x.KeyWords,
				MetaDescription = x.MetaDescription,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				PublishDate = x.PublishDate.ToFarsi(),
				ShortDescription = x.ShortDescription,
				Slug = x.Slug,
				Title = x.Title
			}).AsNoTracking().FirstOrDefault(c => c.Id == id);
		}

		public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
		{
			var query =_context.Articles.Include(c=>c.Category).Select(x => new ArticleViewModel 
			{
				Id = x.Id,
				Title = x.Title,
				ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.Description.Length, 50)) + "...",
				CategoryId = x.CategoryId,
				Category = x.Category.Name,
				Picture = x.Picture,
				PublishDate = x.PublishDate.ToFarsi(),
				CreationDate = x.CreationDate.ToFarsi()
			}).AsNoTracking();

			if (!string.IsNullOrWhiteSpace(searchModel.Title))
				query = query.Where(x => x.Title.Contains(searchModel.Title));

			if (searchModel.CategoryId > 0)
				query = query.Where(x => x.CategoryId == searchModel.CategoryId);

			return query.OrderByDescending(c => c.Id).ToList();
		}
	}
}
