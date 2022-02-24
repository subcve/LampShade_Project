using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        public string Name { get; set; }
    }
}