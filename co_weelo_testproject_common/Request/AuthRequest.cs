using System.ComponentModel.DataAnnotations;

namespace co_weelo_testproject_common.Request
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
