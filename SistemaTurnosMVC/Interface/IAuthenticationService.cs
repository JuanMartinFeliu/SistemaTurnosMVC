using System;
namespace SistemaTurnosMVC.Interface
{
    public interface IAuthenticationService
    {
        bool Login(string user, string pass);
        void Logout();
        bool isAutheticated();
        bool hasAccessLevel(string requiredAccessLevel);
    }
}