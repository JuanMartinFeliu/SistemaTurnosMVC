using System.ComponentModel.DataAnnotations;
using SistemaTurnosMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // Para el SelectList

namespace SistemaTurnosMVC.ViewModels
{
    public class PacienteIndexViewModel
    {
        public int IdPaciente {get;set;}

        [Display(Name ="Nombre del paciente")]
        public string Nombre {get;set;}

        [Display(Name ="Apellido del paciente")]
        public string Apellido {get;set;}
        
        [Display(Name ="DNI del paciente")]
        public string DNI {get;set;}

        [Display(Name ="Fecha de nacimiento del paciente")]
        public string FechaNacimiento {get;set;}

        [Display(Name ="Sexo del paciente")]
        public Sexo Sexo {get;set;}

        [Display(Name ="Telefono del paciente")]
        public string Telefono {get;set;}

        [Display(Name ="Email del paciente")]
        public string Email {get;set;}

        [Display(Name ="Direccion del paciente")]
        public string Direccion {get;set;}

        [Display(Name ="Obra Social del paciente")]
        public ObraSocial ObraSocial {get;set;}

        public List<SelectListItem> ListaSexo { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaObraSocial { get; set; } = new List<SelectListItem>();
    }
}