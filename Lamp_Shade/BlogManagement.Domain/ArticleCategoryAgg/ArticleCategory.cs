using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
	public class ArticleCategory : EntityBase
	{
		public string Name { get; private set; }
		public string Picture { get; private set; }
		public string PictureAlt { get; private set; }
		public string PictureTitle { get; private set; }
		public string KeyWords { get; private set; }
		public string MetaDescription { get; private set; }
		public string Description { get; private set; }
		public string Slug { get; private set; }
		public int ShowOrder { get; private set; }
		public string CanonicalAddress { get; private set; }
		public List<Article> Articles { get; private set; }

		public ArticleCategory(string name, string picture, string keyWords, string metaDescription,
			string description, string slug, int showOrder, string canonicalAddress, string pictureAlt, string pictureTitle)
		{
			Name = name;
			Picture = picture;
			KeyWords = keyWords;
			MetaDescription = metaDescription;
			Description = description;
			Slug = slug;
			ShowOrder = showOrder;
			CanonicalAddress = canonicalAddress;
			PictureAlt = pictureAlt;
			PictureTitle = pictureTitle;
			Articles = new List<Article>();
		}
		public void Edit(string name, string picture, string keyWords, string metaDescription,
			string description, string slug, int showOrder, string canonicalAddress,string pictureAlt, string pictureTitle)
		{
			Name = name;
			Picture = picture;
			KeyWords = keyWords;
			MetaDescription = metaDescription;
			Description = description;
			Slug = slug;
			ShowOrder = showOrder;
			CanonicalAddress = canonicalAddress;
			PictureAlt = pictureAlt;
			PictureTitle = pictureTitle;
		}
	}
}
