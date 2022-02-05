using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(255,ErrorMessage = ValidationMessages.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(15,ErrorMessage = ValidationMessages.MaxLength)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(150,ErrorMessage = ValidationMessages.MaxLength)]
        public string ShortDescription { get; set; }
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        //[MaxLength(1000, ErrorMessage = ValidationMessages.MaxLength)]
        public IFormFile Picture { get; set; }


        [MaxLength(255,ErrorMessage = ValidationMessages.MaxLength)]
        public string PictureAlt { get; set; }


        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100,ErrorMessage = ValidationMessages.MaxLength)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(150,ErrorMessage = ValidationMessages.MaxLength)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string Slug { get; set; }

        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
