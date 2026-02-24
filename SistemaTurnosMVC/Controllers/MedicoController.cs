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
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<PacienteController> _logger;
        public MedicoController(IMedicoRepository medicoRepository, 
                               IAuthenticationService authenticationSevice,
                               ILogger<PacienteController> logger)
        {
            _medicoRepository = medicoRepository;
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
                
                List<Medico> lista = _medicoRepository.GetAll();
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

                var medicoVM = new MedicoCreateViewModel
                {
                    ListaEspecialidad = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Cardiologia", Text = "Cardiologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Pediatria", Text = "Pediatria" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Dermatologia", Text = "Dermatologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Ginecologia", Text = "Ginecologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Traumatologia", Text = "Traumatologia" }
                    }
                };

                return View(medicoVM); // Se pasa el modelo a la vista para que @Model no sea null
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult Alta(MedicoCreateViewModel medicoVM)
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
                        return View(medicoVM);
                    }

                    var nuevoMedico = new Medico
                    {
                        Nombre = medicoVM.Nombre,
                        Apellido = medicoVM.Apellido,
                        DNI = medicoVM.DNI,
                        Matricula = medicoVM.Matricula,
                        Especialidad = medicoVM.Especialidad,
                        Telefono = medicoVM.Telefono,
                        Email = medicoVM.Email,
                        PrecioConsulta = medicoVM.PrecioConsulta
                    };

                    _medicoRepository.Add(nuevoMedico);
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

                var medico = _medicoRepository.GetById(id);
                if(medico == null)
                {
                    return NotFound();
                }
                
                // hago el mapeo manual a prodVM

                var medicoVM = new MedicoUpdateViewModel
                {
                    ListaEspecialidad = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
                    {
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Cardiologia", Text = "Cardiologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Pediatria", Text = "Pediatria" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Dermatologia", Text = "Dermatologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Ginecologia", Text = "Ginecologia" },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "Traumatologia", Text = "Traumatologia" }
                    }
                };

                return View(medicoVM); // Se pasa el modelo a la vista para que @Model no sea null
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Modificar(int id, MedicoUpdateViewModel medicoVM)
        {
            try
            {
                if(_authenticationService.hasAccessLevel("Administrador"))
                {
                    if(id != medicoVM.IdMedico)
                    {
                        return NotFound();
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(medicoVM);
                    }

                    // Mapeo Manual de VM a Modelo de Dominio
                    var medicoAEditar = new Medico
                    {
                        Nombre = medicoVM.Nombre,
                        Apellido = medicoVM.Apellido,
                        DNI = medicoVM.DNI,
                        Matricula = medicoVM.Matricula,
                        Especialidad = medicoVM.Especialidad,
                        Telefono = medicoVM.Telefono,
                        Email = medicoVM.Email,
                        PrecioConsulta = medicoVM.PrecioConsulta
                    };

                    _medicoRepository.Update(medicoAEditar,id);
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

                var medico = _medicoRepository.GetById(id);

                if(medico == null)
                {
                    return NotFound();
                }

                return View(medico);
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
                    _medicoRepository.Delete(id);
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

    }
}