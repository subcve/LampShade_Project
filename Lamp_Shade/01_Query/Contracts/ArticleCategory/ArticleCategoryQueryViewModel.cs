using _01_Query.Contracts.Article;

namespace _01_Query.Contracts.ArticleCategory
{
	public class ArticleCategoryQueryViewModel
	{
		public string Name { get; set; }
		public string Picture { get; set; }
		public string PictureAlt { get; set; }
		public string PictureTitle { get; set; }
		public string KeyWords { get; set; }
		public string MetaDescription { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public int ShowOrder { get; set; }
		public string? CanonicalAddress { get; set; }
		public long ArticlesCount { get; set; }
		public List<string> KeywordList { get; set; }
		public List<ArticleQueryViewModel> Articles { get; set; }
	}
}
