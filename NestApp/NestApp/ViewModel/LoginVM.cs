using System.ComponentModel.DataAnnotations;

namespace NestApp.ViewModel
{
    public class LoginVM
    {
        public string UserNameorEmail { get; set; } = null!;
        [MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
