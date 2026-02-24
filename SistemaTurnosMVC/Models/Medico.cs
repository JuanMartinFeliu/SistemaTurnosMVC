using System;

namespace SistemaTurnosMVC.Models
{
    public enum Especialidad
{
    Cardiologia,
    Pediatria,
    Dermatologia,
    Ginecologia,
    Traumatologia
}
    public class Medico
    {
        public int IdMedico {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public string DNI {get;set;}
        public string Matricula {get;set;}
        public Especialidad Especialidad {get;set;}
        public string Telefono {get;set;}
        public string Email {get;set;}
        public double PrecioConsulta {get;set;}
    }
}
