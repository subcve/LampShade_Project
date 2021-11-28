
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, 99999999, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
        [Range(1,99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500 , ErrorMessage =ValidationMessages.MaxLength)]
        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
