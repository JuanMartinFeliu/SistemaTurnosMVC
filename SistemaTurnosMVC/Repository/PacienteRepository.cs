using SistemaTurnosMVC.Models;
using SistemaTurnosMVC.Interface;
using Microsoft.Data.Sqlite;

namespace SistemaTurnosMVC.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string _cadenaConexion;
        public PacienteRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }
        public List<Paciente> GetAll()
        {
            Paciente p = null;
            var lista = new List<Paciente>();
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"SELECT IdPaciente,
                                  Nombre, 
                                  Apellido, 
                                  DNI,
                                  FechaNacimiento,
                                  Sexo,
                                  Telefono,
                                  Email,
                                  Direccion,
                                  ObraSocial
                                  FROM Paciente;";

            try{
                using var comando = new SqliteCommand(sql,conexion);
                using var lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    p = new Paciente()
                    {
                        IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                        Nombre = Convert.ToString(lector["Nombre"]),
                        Apellido = Convert.ToString(lector["Apellido"]),
                        DNI = Convert.ToString(lector["DNI"]),
                        FechaNacimiento = Convert.ToDateTime(lector["FechaNacimiento"]),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), lector["Sexo"].ToString()),
                        Telefono = Convert.ToString(lector["Telefono"]),
                        Email = Convert.ToString(lector["Email"]),
                        Direccion = Convert.ToString(lector["Direccion"]),
                        ObraSocial = (ObraSocial)Enum.Parse(typeof(ObraSocial), lector["ObraSocial"].ToString())
                    };

                    lista.Add(p);
                }
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en GetAll");
            }
            if (p == null)
            {
                throw new Exception("Paciente inexistente");
            }

            return lista;
        }

        public List<Paciente> FiltrarPorSexo(string sexo)
        {
            Paciente p = null;
            var lista = new List<Paciente>();
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"SELECT IdPaciente,
                                  Nombre, 
                                  Apellido, 
                                  DNI,
                                  FechaNacimiento,
                                  Sexo,
                                  Telefono,
                                  Email,
                                  Direccion,
                                  ObraSocial
                                  FROM Paciente
                                  WHERE Sexo = @Sexo;";

            try{
                using var comando = new SqliteCommand(sql,conexion);
                comando.Parameters.AddWithValue("@Sexo", sexo);
                using var lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    p = new Paciente()
                    {
                        IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                        Nombre = Convert.ToString(lector["Nombre"]),
                        Apellido = Convert.ToString(lector["Apellido"]),
                        DNI = Convert.ToString(lector["DNI"]),
                        FechaNacimiento = Convert.ToDateTime(lector["FechaNacimiento"]),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), lector["Sexo"].ToString()),
                        Telefono = Convert.ToString(lector["Telefono"]),
                        Email = Convert.ToString(lector["Email"]),
                        Direccion = Convert.ToString(lector["Direccion"]),
                        ObraSocial = (ObraSocial)Enum.Parse(typeof(ObraSocial), lector["ObraSocial"].ToString())
                    };

                    lista.Add(p);
                }
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en FiltrarPorSexo");
            }
            if (p == null)
            {
                throw new Exception("Paciente inexistente");
            }

            return lista;
        }
        public Paciente GetById(int idBuscado)
        {
            Paciente p = null;
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"SELECT IdPaciente,
                                  Nombre, 
                                  Apellido, 
                                  DNI,
                                  FechaNacimiento,
                                  Sexo,
                                  Telefono,
                                  Email,
                                  Direccion,
                                  ObraSocial
                                  FROM Paciente 
                                  WHERE IdPaciente = @Id;";

            try
            {
                using var comando = new SqliteCommand(sql,conexion);
                comando.Parameters.AddWithValue("@Id", idBuscado);

                using var lector = comando.ExecuteReader();

                if(lector.Read())
                {
                    p = new Paciente()
                    {
                        IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                        Nombre = Convert.ToString(lector["Nombre"]),
                        Apellido = Convert.ToString(lector["Apellido"]),
                        DNI = Convert.ToString(lector["DNI"]),
                        FechaNacimiento = Convert.ToDateTime(lector["FechaNacimiento"]),
                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), lector["Sexo"].ToString()),
                        Telefono = Convert.ToString(lector["Telefono"]),
                        Email = Convert.ToString(lector["Email"]),
                        Direccion = Convert.ToString(lector["Direccion"]),
                        ObraSocial = (ObraSocial)Enum.Parse(typeof(ObraSocial), lector["ObraSocial"].ToString())
                    };

                    return p; 
                }
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en GetById");
            }
            if (p == null)
            {
                throw new Exception("Paciente inexistente");
            }

            return null;
        }
        public void Add(Paciente paciente)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"INSERT INTO Paciente (Nombre, Apellido, DNI, FechaNacimiento, Sexo, Telefono, Email, Direccion, ObraSocial) 
                           VALUES (@Nombre, @Apellido, @DNI, @FechaNacimiento, @Sexo, @Telefono, @Email, @Direccion, @ObraSocial)";


            try
            {
                using var comando = new SqliteCommand(sql,conexion);

                comando.Parameters.Add(new SqliteParameter("@Nombre",paciente.Nombre));
                comando.Parameters.Add(new SqliteParameter("@Apellido",paciente.Apellido));
                comando.Parameters.Add(new SqliteParameter("@DNI",paciente.DNI));
                comando.Parameters.Add(new SqliteParameter("@FechaNacimiento",paciente.FechaNacimiento));
                comando.Parameters.Add(new SqliteParameter("@Sexo",paciente.Sexo));
                comando.Parameters.Add(new SqliteParameter("@Telefono",paciente.Telefono));
                comando.Parameters.Add(new SqliteParameter("@Email",paciente.Email));
                comando.Parameters.Add(new SqliteParameter("@Direccion",paciente.Direccion));
                comando.Parameters.Add(new SqliteParameter("@ObraSocial",paciente.ObraSocial));

                comando.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en Add");
            }
        }
        public void Update(Paciente paciente, int idBuscado)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"UPDATE Paciente SET Nombre = @Nombre,
                                               Apellido = @Apellido,
                                               DNI = @DNI, 
                                               FechaNacimiento = @FechaNacimiento, 
                                               Sexo = @Sexo, 
                                               Telefono = @Telefono, 
                                               Email = @Email,
                                               Direccion = @Direccion,
                                               ObraSocial = @ObraSocial 
                                               WHERE IdPaciente = @IdPaciente;";

            try
            {
                using var comando = new SqliteCommand(sql,conexion);

                comando.Parameters.Add(new SqliteParameter("@Nombre",paciente.Nombre));
                comando.Parameters.Add(new SqliteParameter("@Apellido",paciente.Apellido));
                comando.Parameters.Add(new SqliteParameter("@DNI",paciente.DNI));
                comando.Parameters.Add(new SqliteParameter("@FechaNacimiento",paciente.FechaNacimiento));
                comando.Parameters.Add(new SqliteParameter("@Sexo",paciente.Sexo));
                comando.Parameters.Add(new SqliteParameter("@Telefono",paciente.Telefono));
                comando.Parameters.Add(new SqliteParameter("@Email",paciente.Email));
                comando.Parameters.Add(new SqliteParameter("@Direccion",paciente.Direccion));
                comando.Parameters.Add(new SqliteParameter("@ObraSocial",paciente.ObraSocial));
                comando.Parameters.AddWithValue("@IdPaciente", idBuscado);

                comando.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en Update");
            }
        }
        public void Delete(int idBuscado)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = "DELETE FROM Paciente WHERE IdPaciente = @IdPaciente";

            try
            {
                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.Add(new SqliteParameter("@IdPaciente",idBuscado));
                comando.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos Delete");
            }
        }
    }
}