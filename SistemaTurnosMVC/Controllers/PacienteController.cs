using System;
using SistemaTurnosMVC.Models;
using SistemaTurnosMVC.Repository;
using SistemaTurnosMVC.ViewModels;
using SistemaTurnosMVC.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Para el SelectList
using System.Collections.Generic;

namespace Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<PacienteController> _logger;
        public PacienteController(IPacienteRepository pacienteRepository, 
                               IAuthenticationService authenticationSevice,
                               ILogger<PacienteController> logger)
        {
            _pacienteRepository = pacienteRepository;
            _authenticationService = authenticationSevice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index","Login");
                }
                
                List<Paciente> lista = _pacienteRepository.GetAll();
                return View(lista);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        /*---------------------------------------------------------------------*/

        [HttpGet]
        public IActionResult Alta()
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index", "Login");
                }

                if(!_authenticationService.hasAccessLevel("Administrador"))
                {
                    return RedirectToAction("AccesoDenegado");
                }

                var pacienteVM = new PacienteCreateViewModel
                {
                    ListaSexo = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Masculino", Text = "Masculino" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Femenino", Text = "Femenino" },
                    },

                    ListaObraSocial = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "OSDE", Text = "OSDE" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SwissMedical", Text = "SwissMedical" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Galeno", Text = "Galeno" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SancorSalud", Text = "SancorSalud" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Medicus", Text = "Medicus" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SinObraSocial", Text = "SinObraSocial" },
                    }
                };

                return View(pacienteVM); // Se pasa el modelo a la vista para que @Model no sea null
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult Alta(PacienteCreateViewModel pacienteVM)
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index","Login");
                }
                
                if(_authenticationService.hasAccessLevel("Administrador"))
                {
                    if (!ModelState.IsValid)
                    {
                        return View(pacienteVM);
                    }

                    var nuevoPaciente = new Paciente
                    {
                        Nombre = pacienteVM.Nombre,
                        Apellido = pacienteVM.Apellido,
                        DNI = pacienteVM.DNI,
                        FechaNacimiento = Convert.ToDateTime(pacienteVM.FechaNacimiento),
                        Sexo = pacienteVM.Sexo,
                        Telefono = pacienteVM.Telefono,
                        Email = pacienteVM.Email,
                        Direccion = pacienteVM.Direccion,
                        ObraSocial = pacienteVM.ObraSocial
                        
                    };

                    _pacienteRepository.Add(nuevoPaciente);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }

            return RedirectToAction("AccesoDenegado");
        }

        /*---------------------------------------------------------------------*/

        [HttpGet]
        public IActionResult Modificar(int id)
        {
            try
            {
                if(!_authenticationService.hasAccessLevel("Administrador"))
                {
                    return RedirectToAction("AccesoDenegado");
                }

                var paciente = _pacienteRepository.GetById(id);
                if(paciente == null)
                {
                    return NotFound();
                }
                
                // hago el mapeo manual a prodVM

                var pacienteVM = new PacienteUpdateViewModel
                {
                    ListaSexo = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Masculino", Text = "Masculino" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Femenino", Text = "Femenino" },
                    },

                    ListaObraSocial = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "OSDE", Text = "OSDE" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SwissMedical", Text = "SwissMedical" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Galeno", Text = "Galeno" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SancorSalud", Text = "SancorSalud" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Medicus", Text = "Medicus" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "SinObraSocial", Text = "SinObraSocial" },
                    }
                };

                return View(pacienteVM); // Se pasa el modelo a la vista para que @Model no sea null
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Modificar(int id, PacienteUpdateViewModel pacienteVM)
        {
            try
            {
                if(_authenticationService.hasAccessLevel("Administrador"))
                {
                    if(id != pacienteVM.IdPaciente)
                    {
                        return NotFound();
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(pacienteVM);
                    }

                    // Mapeo Manual de VM a Modelo de Dominio
                    var pacienteAEditar = new Paciente
                    {
                        Nombre = pacienteVM.Nombre,
                        Apellido = pacienteVM.Apellido,
                        DNI = pacienteVM.DNI,
                        FechaNacimiento = Convert.ToDateTime(pacienteVM.FechaNacimiento),
                        Sexo = pacienteVM.Sexo,
                        Telefono = pacienteVM.Telefono,
                        Email = pacienteVM.Email,
                        Direccion = pacienteVM.Direccion,
                        ObraSocial = pacienteVM.ObraSocial
                    };

                    _pacienteRepository.Update(pacienteAEditar,id);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
            if (!_authenticationService.isAutheticated())
            {
                return RedirectToAction("Index","Login");
            }
            
            return RedirectToAction("AccesoDenegado");
        }

        /*---------------------------------------------------------------------*/

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index", "Login");
                }

                if(!_authenticationService.hasAccessLevel("Administrador"))
                {
                    return RedirectToAction("AccesoDenegado");
                }

                var paciente = _pacienteRepository.GetById(id);

                if(paciente == null)
                {
                    return NotFound();
                }

                return View(paciente);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")] // Esto permite que el formulario apunte a "Delete" pero el método se llame distinto
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index","Login");
                }
                
                if(_authenticationService.hasAccessLevel("Administrador"))
                {
                    _pacienteRepository.Delete(id);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }

            return RedirectToAction("AccesoDenegado");
        }

        /*-------------------------------------------------------------------------------------------------------*/

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        /*------------------------------------------------------------------------------------------------------------*/

        [HttpGet]
        public IActionResult Filtro(string sexo)
        {
            try
            {
                if (!_authenticationService.isAutheticated())
                {
                    return RedirectToAction("Index","Login");
                }
                
                List<Paciente> lista;

                if (string.IsNullOrEmpty(sexo))
                {
                    lista = _pacienteRepository.GetAll();
                }
                else
                {
                    lista = _pacienteRepository.FiltrarPorSexo(sexo);
                }

                return View(lista);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }
    }
}