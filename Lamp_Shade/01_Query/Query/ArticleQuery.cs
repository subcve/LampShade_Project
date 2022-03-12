using _01_Framework.Application;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.Comments;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace _01_Query.Query
{
	public class ArticleQuery : IArticleQuery
	{
		private readonly BlogContext _context;
		private readonly CommentContext _commentContext;
		public ArticleQuery(BlogContext context, CommentContext commentContext)
		{
			_context = context;
			_commentContext = commentContext;
		}

		public ArticleQueryViewModel GetArticleDetails(string slug)
		{
			var article = _context.Articles
				.Include(z => z.Category)
				.Where(c => c.PublishDate <= DateTime.Now)
				.Select(x => new ArticleQueryViewModel
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
				}).AsNoTracking().FirstOrDefault(c => c.Slug == slug);

			if (article.KeyWords.Contains(","))
				article.KeywordList = article.KeyWords.Split(",").ToList();

			var comments = _commentContext.Comments
				.Where(c => c.Type == CommentType.Article && !c.IsCanceled && c.IsConfirmed && c.OwnerRecordId == article.Id)
				.Select(z => new CommentQueryModel
				{
					Id = z.Id,
					Message = z.Message,
					Name = z.Name,
					ParentId = z.ParentId,
					CreationDate = z.CreationDate.ToFarsi()
				}).OrderByDescending(x => x.Id).ToList();
			foreach (var comment in comments)
			{
				if (comment.ParentId > 0)
					comment.ParentName = comments.FirstOrDefault(c => c.Id == comment.ParentId).Name;
			}
			article.Comments = comments;
			return article;
		}

		public List<ArticleQueryViewModel> GetLatestArticles()
		{
			return _context.Articles.Include(z => z.Category).Where(c => c.PublishDate <= DateTime.Now).Select(x => new ArticleQueryViewModel
			{
				Id = x.Id,
				PictureAlt = x.PictureAlt,
				PictureTitle = x.PictureTitle,
				PublishDate = x.PublishDate.ToFarsi(),
				ShortDescription = x.ShortDescription,
				Title = x.Title,
				Picture = x.Picture,
				Slug = x.Slug
			}).AsNoTracking().OrderByDescending(c => c.Id).Take(3).ToList();
		}
	}
}
