using System.ComponentModel.DataAnnotations;

namespace SistemaTurnosMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        [Required]
        public string User {get;set;}

        [Display(Name = "Contraseña")]
        [Required]
        [DataType(DataType.Password)] //muestra la contraseña con asteriscos al ingresarse
        public string Password {get;set;}
    }
}