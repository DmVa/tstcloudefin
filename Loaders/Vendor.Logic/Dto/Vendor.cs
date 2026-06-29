using System.ComponentModel.DataAnnotations;

namespace Vendor.Logic.Dto
{
    public record Vendor
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;
    }
}
