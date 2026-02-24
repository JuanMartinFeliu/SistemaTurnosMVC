using System;
using SistemaTurnosMVC.Interface;

namespace SistemaTurnosMVC.Services;

public class AutheticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutheticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool Login(string username, string pass)
    {
        var context = _httpContextAccessor.HttpContext;
        var user = _userRepository.GetUser(username,pass);

        if(user != null)
        {
            if(context == null)
            {
                throw new InvalidOperationException("HttpContext no está disponible.");
            }

            context.Session.SetString("IsAuthenticated","true");
            context.Session.SetString("User",user.User);
            context.Session.SetString("UserNombre",user.Nombre);
            context.Session.SetString("Rol",user.Rol);

            return true;
        }

        return false;
    }
    public void Logout()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            throw new InvalidOperationException("HttpContext no está disponible.");
        }
        /* context.Session.Remove("IsAuthenticated");
        context.Session.Remove("User");
        context.Session.Remove("UserNombre");
        context.Session.Remove("Rol");
        */
        context.Session.Clear();
    }
    public bool isAutheticated()
    {
        var context = _httpContextAccessor.HttpContext;
        if(context == null)
        {
            throw new InvalidOperationException("HttpContext no está disponible.");
        }
        return context.Session.GetString("IsAuthenticated") == "true";
    }
    public bool hasAccessLevel(string requiredAccessLevel)
    {
        var context = _httpContextAccessor.HttpContext;
        if(context == null)
        {
            throw new InvalidOperationException("HttpContext no está disponible.");
        }

        return context.Session.GetString("Rol") == requiredAccessLevel;
    }
}