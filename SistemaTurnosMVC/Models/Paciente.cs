using System;

namespace SistemaTurnosMVC.Models
{
    public enum Sexo
    {
        Masculino,
        Femenino
    }

    public enum ObraSocial
    {
        OSDE, 
        SwissMedical, 
        Galeno, 
        SancorSalud,
        Medicus,
        SinObraSocial
    }
    public class Paciente
    {
        public int IdPaciente {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public string DNI {get;set;}
        public DateTime FechaNacimiento {get;set;}
        public Sexo Sexo {get;set;}
        public string Telefono {get;set;}
        public string Email {get;set;}
        public string Direccion {get;set;}
        public ObraSocial ObraSocial {get;set;}
    }
}