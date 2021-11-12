using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(0,100000 , ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(1000,ErrorMessage = ValidationMessages.MaxLength)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string PictureTitle { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
