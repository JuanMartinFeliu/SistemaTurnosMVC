using SistemaTurnosMVC.Models;

namespace SistemaTurnosMVC.Interface 
{
    public interface IUserRepository
    {
        public Usuario GetUser(string username, string password);
    }
}