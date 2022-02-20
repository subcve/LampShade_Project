using _01_Query.Contracts.Comments;

namespace _01_Query.Contracts.Article
{
	public class ArticleQueryViewModel
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Picture { get; set; }
		public string PictureAlt { get; set; }
		public string PictureTitle { get; set; }
		public string PublishDate { get; set; }
		public string KeyWords { get; set; }
		public List<string> KeywordList { get; set; }
		public string MetaDescription { get; set; }
		public string Slug { get; set; }
		public string? CanonicalAddress { get; set; }
		public long CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string CategorySlug { get; set; }
		public List<CommentQueryModel> Comments { get; set; }
	}
}
