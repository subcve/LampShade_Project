using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlogManagement.Infrastructure.EFCore.Repository
{
	public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
	{
		private readonly BlogContext _context;
		public ArticleCategoryRepository(BlogContext context) : base(context)
		{
			_context = context;
		}

		public List<ArticleCategoryViewModel> GetArticleCategories()
		{
			return _context.ArticleCategories.Select(c => new ArticleCategoryViewModel
			{ Id = c.Id, Name = c.Name }).AsNoTracking().ToList();
		}

		public EditArticleCategory GetDetails(long id)
		{
			return _context.ArticleCategories.Select(x => new EditArticleCategory
			{
				Id = x.Id,
				Name = x.Name,
				CanonicalAddress = x.CanonicalAddress,
				KeyWords = x.KeyWords,
				Description = x.Description.Substring(0,Math.Min(x.Description.Length,50)) + "...",
				MetaDescription = x.MetaDescription,
				ShowOrder = x.ShowOrder,
				Slug = x.Slug,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle
			}).FirstOrDefault(x => x.Id == id);
		}

		public string GetSlugBy(long id)
		{
			return _context.ArticleCategories.Select(c => new { c.Id, c.Slug }).FirstOrDefault(c => c.Id == id).Slug;
		}

		public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
		{
			var query = _context.ArticleCategories.Include(c=>c.Articles).Select(c=> new ArticleCategoryViewModel
			{
				Id = c.Id,
				Name = c.Name,
				Picture = c.Picture,
				Description = c.Description,
				ShowOrder = c.ShowOrder,
				CreationDate = c.CreationDate.ToFarsi(),
				ArticleCount = c.Articles.Count(),
			}).AsNoTracking();

			if (!string.IsNullOrWhiteSpace(searchModel.Name))
				query = query.Where(c => c.Name.Contains(searchModel.Name));
			return query.OrderByDescending(c => c.ShowOrder).ToList();
		}
	}
}
