using _0_Framework.Application;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
	public class ArticleCategoryQuery : IArticleCategoryQuery
	{
		private readonly BlogContext _context;

		public ArticleCategoryQuery(BlogContext context)
		{
			_context = context;
		}

		public List<ArticleCategoryQueryViewModel> GetArticleCategories()
		{
			return _context.ArticleCategories.Include(z=>z.Articles).Select(x => new ArticleCategoryQueryViewModel
			{
				Name = x.Name,
				ShowOrder = x.ShowOrder,
				Slug = x.Slug,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				Picture = x.Picture,
				ArticlesCount = x.Articles.Count
			}).AsNoTracking().OrderBy(c=>c.ShowOrder).ToList();
		}

        public ArticleCategoryQueryViewModel GetArticleCategory(string slug)
        {
            var articleCategory = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryViewModel
                {
                    Slug = x.Slug,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticlesCount = x.Articles.Count,
                    Articles = MapArticles(x.Articles)
                }).FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(articleCategory.KeyWords))
                articleCategory.KeywordList = articleCategory.KeyWords.Split(",").ToList();

            return articleCategory;
        }

        private static List<ArticleQueryViewModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryViewModel
            {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
