using _01_Framework.Application;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
	public class CreateArticleCategory
	{
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
		public string Name { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxFileSize(3 * 1024 *1024 ,ErrorMessage = ValidationMessages.MaxFileSize)]
		public IFormFile Picture { get; set; }
		[MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public string PictureAlt { get; set; }
		[MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public string PictureTitle { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
		public string KeyWords { get; set; }
		[MaxLength(150, ErrorMessage = ValidationMessages.MaxLength)]
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public string MetaDescription { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(2000, ErrorMessage = ValidationMessages.MaxLength)]
		public string Description { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		[MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
		public string Slug { get; set; }
		[Required(ErrorMessage = ValidationMessages.IsRequired)]
		public int ShowOrder { get; set; }
		[MaxLength(1000, ErrorMessage = ValidationMessages.MaxLength)]
		public string? CanonicalAddress { get; set; }
	}
}
