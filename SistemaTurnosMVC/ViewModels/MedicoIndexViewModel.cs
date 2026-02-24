using System.ComponentModel.DataAnnotations;
using SistemaTurnosMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // Para el SelectList

namespace SistemaTurnosMVC.ViewModels
{
    public class MedicoIndexViewModel
    {
        public int IdMedico {get;set;}

        [Display(Name ="Nombre del medico")]
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [StringLength(80,MinimumLength = 1,ErrorMessage ="El nombre debe tener entre 1 y 80 caracteres")]
        public string Nombre {get;set;}

        [Display(Name ="Apellido del medioc")]
        [Required(ErrorMessage ="El Apellido es obligatorio")]
        [StringLength(80,MinimumLength = 1,ErrorMessage ="El apellido debe tener entre 1 y 80 caracteres")]
        public string Apellido {get;set;}
        
        [Display(Name ="DNI del medico")]
        [Required(ErrorMessage ="El DNI es obligatorio")]
        [StringLength(12,MinimumLength = 7,ErrorMessage ="El DNI debe tener entre 7 y 12 caracteres")]
        public string DNI {get;set;}

        [Display(Name ="Matricula del medico")]
        [Required(ErrorMessage ="La fecha de nacimiento es obligatoria")]
        [StringLength(30,MinimumLength = 10,ErrorMessage ="La matricula debe tener entre 10 y 30 caracteres")]
        public string Matricula {get;set;}

        [Display(Name ="Especialidad del medico")]
        [Required(ErrorMessage ="La especialidad es obligatoria")]
        public Especialidad Especialidad {get;set;}

        [Display(Name ="Telefono del medico")]
        [StringLength(25,MinimumLength = 1,ErrorMessage ="El telefono debe tener entre 1 y 25 caracteres")]
        public string Telefono {get;set;}

        [Display(Name ="Email del medico")]
        [StringLength(50,MinimumLength = 10,ErrorMessage ="El Email debe tener entre 10 y 50 caracteres")]
        public string Email {get;set;}

        public List<SelectListItem> ListaEspecialidad { get; set; } = new List<SelectListItem>();
    }
}