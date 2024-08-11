using System.ComponentModel.DataAnnotations;

namespace Shared.Lib.DTOs
{
    public class RegisterDto : AccountBaseDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
