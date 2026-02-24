using System;
using SistemaTurnosMVC.Models;
using SistemaTurnosMVC.Repository;
using SistemaTurnosMVC.ViewModels;
using SistemaTurnosMVC.Interface;
using SistemaTurnosMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class LoginController : Controller
    {
        // INYECTO IAuthenticationSevice
        private readonly IAuthenticationService _authenticationSevice;
        
        // INYECTO ILogger
        private readonly ILogger<LoginController> _logger;

        public LoginController(IAuthenticationService authenticationSevice , ILogger<LoginController> logger)
        {
            _authenticationSevice = authenticationSevice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                LoginViewModel model = new LoginViewModel();
                return View(model);
            }
            catch(Exception ex)
            {
                // Errores en ejecución: Log Error con serialización ToString()
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.User) || string.IsNullOrEmpty(model.Password))
                {
                    return View("Index",model);
                }

                if (_authenticationSevice.Login(model.User, model.Password))
                {
                    // Acceso Exitoso = LogInformation
                    _logger.LogInformation("El usuario {Usuario} ingresó correctamente", model.User);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    // Acceso rechazado = LogWarning
                    _logger.LogWarning("Intento de acceso inválido + Usuario: {Usuario} + Clave ingresada: {Clave}", model.User, model.Password);
                }
            }
            catch(Exception ex)
            {
                // Errores en ejecución: Log Error con serialización ToString()
                _logger.LogError(ex.ToString());
                return View("Error");
            }

            return View("Index",model); 
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                _authenticationSevice.Logout();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // Errores en ejecución: Log Error con serialización ToString()
                _logger.LogError(ex.ToString());
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}