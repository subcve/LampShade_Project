using _0_Framework.Application;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.Article
{
	public class CreateArticle
	{
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
		public string Title { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public string Description { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(1000,ErrorMessage = ValidationMessages.MaxLength)]

		public string ShortDescription { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxFileSize(3 * 1024 *1024 ,ErrorMessage = ValidationMessages.MaxFileSize)]
		public IFormFile Picture { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
		public string PictureAlt { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
		public string PictureTitle { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public string PublishDate { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
		public string KeyWords { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLength)]
		public string MetaDescription { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
		public string Slug { get; set; }
		[MaxLength(1000, ErrorMessage = ValidationMessages.MaxLength)]
		public string CanonicalAddress { get; set; }
		[Range(1,long.MaxValue,ErrorMessage = ValidationMessages.IsRequired)]
		public long CategoryId { get; set; }
	}
}
