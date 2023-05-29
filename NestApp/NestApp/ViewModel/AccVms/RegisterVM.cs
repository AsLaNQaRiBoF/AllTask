using System.ComponentModel.DataAnnotations;

namespace NestApp.ViewModel.AccVms
{
    public class RegisterVM
    {
        public string Name { get; set; }= null!;
        public string SurName { get; set; }= null!;
        public string UserName { get; set; }= null!;
        [MaxLength(100),DataType(DataType.EmailAddress)]    
        public string Email { get; set; }=null!;
        [MaxLength(100), DataType(DataType.Password)]
        public string Pasword { get; set; } = null!;
        [MaxLength(100), DataType(DataType.Password), Compare(nameof(Pasword))]
        public string ConfirmedPAswoord { get; set; } = null!;
    }
}
