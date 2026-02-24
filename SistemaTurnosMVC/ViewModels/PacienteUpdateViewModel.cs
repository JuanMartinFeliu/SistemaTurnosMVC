using System.ComponentModel.DataAnnotations;
using SistemaTurnosMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // Para el SelectList

namespace SistemaTurnosMVC.ViewModels
{
    public class PacienteUpdateViewModel
    {
        [Display(Name ="Id del Paciente")]
        [Required(ErrorMessage ="El id es obligatorio")]
        public int IdPaciente {get;set;}

        [Display(Name ="Nombre del paciente")]
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [StringLength(80,MinimumLength = 1,ErrorMessage ="El nombre debe tener entre 1 y 80 caracteres")]
        public string Nombre {get;set;}

        [Display(Name ="Apellido del paciente")]
        [Required(ErrorMessage ="El Apellido es obligatorio")]
        [StringLength(80,MinimumLength = 1,ErrorMessage ="El apellido debe tener entre 1 y 80 caracteres")]
        public string Apellido {get;set;}
        
        [Display(Name ="DNI del paciente")]
        [Required(ErrorMessage ="El DNI es obligatorio")]
        [StringLength(12,MinimumLength = 7,ErrorMessage ="El DNI debe tener entre 7 y 12 caracteres")]
        public string DNI {get;set;}

        [Display(Name ="Fecha de nacimiento del paciente")]
        [Required(ErrorMessage ="La fecha de nacimiento es obligatoria")]
        [StringLength(20,MinimumLength = 10,ErrorMessage ="La fecha de nacimiento debe tener entre 10 y 20 caracteres")]
        public string FechaNacimiento {get;set;}

        [Display(Name ="Sexo del paciente")]
        [Required(ErrorMessage ="Es obligatorio")]
        public Sexo Sexo {get;set;}

        [Display(Name ="Telefono del paciente")]
        [StringLength(25,MinimumLength = 1,ErrorMessage ="El telefono debe tener entre 1 y 25 caracteres")]
        public string Telefono {get;set;}

        [Display(Name ="Email del paciente")]
        [StringLength(50,MinimumLength = 10,ErrorMessage ="El Email debe tener entre 10 y 50 caracteres")]
        public string Email {get;set;}

        [Display(Name ="Direccion del paciente")]
        [StringLength(50,MinimumLength = 10,ErrorMessage ="La direccion debe tener entre 10 y 50 caracteres")]
        public string Direccion {get;set;}

        [Display(Name ="Obra Social del paciente")]
        [Required(ErrorMessage ="Es obligatorio")]
        public ObraSocial ObraSocial {get;set;}

        public List<SelectListItem> ListaSexo { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaObraSocial { get; set; } = new List<SelectListItem>();
    }
}