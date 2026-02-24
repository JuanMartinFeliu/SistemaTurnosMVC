using SistemaTurnosMVC.Interface;
using System;
using Microsoft.Data.Sqlite;
using SistemaTurnosMVC.Models;

namespace Biblioteca.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _cadenaConexion;

        public UserRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public Usuario GetUser(string username, string password)
        {
            Usuario user = null;

            //Consulta SQL que busca por Usuario Y Contrasena
            const string sql = @"
            SELECT Id, Nombre, User, Password, Rol
            FROM Usuarios
            WHERE User = @Usuario AND Password = @Contrasena";

            try
            {
                using var conexion = new SqliteConnection(_cadenaConexion);
                conexion.Open();

                using var comando = new SqliteCommand(sql,conexion);

                // Se usan parámetros para prevenir inyección SQL
                comando.Parameters.AddWithValue("@Usuario", username);
                comando.Parameters.AddWithValue("@Contrasena", password);

                using var reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    user = new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        User = reader.GetString(2),
                        Password = reader.GetString(3),
                        Rol = reader.GetString(4)
                    };
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos");
            }

            if(user == null)
            {
                throw new Exception("El usuario no existe");
            }

            return user;
        }
    }
}