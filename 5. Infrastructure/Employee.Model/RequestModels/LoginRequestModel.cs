using System.ComponentModel.DataAnnotations;

namespace Employee.Model.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
