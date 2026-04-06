using System.ComponentModel.DataAnnotations;

namespace PresentationLayerWEBApi.DTOs_External
{
    public class RegisterMemberDTo
    {
        [Required]
        [StringLength(50), MinLength(2)]
        public string Name { get; set; } = default!;

        [StringLength(200)]
        public string Description { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(100), MinLength(5)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;

        [Required]
        [Phone]
        [StringLength(20), MinLength(9)]
        public string Phone { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$",
            ErrorMessage = "Password must contain upper, lower, and a number.")]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = default!;

    }
    public class UpdateMemberRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;

        [StringLength(200)]
        public string Description { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = default!;

        [Required]
        [Phone]
        [StringLength(50)]
        public string Phone { get; set; } = default!;

    }
}
