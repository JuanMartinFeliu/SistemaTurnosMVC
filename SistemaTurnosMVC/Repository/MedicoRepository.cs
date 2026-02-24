using SistemaTurnosMVC.Models;
using SistemaTurnosMVC.Interface;
using Microsoft.Data.Sqlite;

namespace SistemaTurnosMVC.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly string _cadenaConexion;
        public MedicoRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }
        public List<Medico> GetAll()
        {
            Medico m = null;
            var lista = new List<Medico>();
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"SELECT IdMedico,
                                  Nombre, 
                                  Apellido, 
                                  DNI,
                                  Matricula,
                                  Especialidad,
                                  Telefono,
                                  Email,
                                  PrecioConsulta
                                  FROM Medico;";

            try{
                using var comando = new SqliteCommand(sql,conexion);
                using var lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    m = new Medico()
                    {
                        IdMedico = Convert.ToInt32(lector["IdMedico"]),
                        Nombre = Convert.ToString(lector["Nombre"]),
                        Apellido = Convert.ToString(lector["Apellido"]),
                        DNI = Convert.ToString(lector["DNI"]),
                        Matricula = Convert.ToString(lector["Matricula"]),
                        Especialidad = Enum.Parse<Especialidad>(lector["Especialidad"].ToString()),
                        Telefono = Convert.ToString(lector["Telefono"]),
                        Email = Convert.ToString(lector["Email"]),
                        PrecioConsulta = Convert.ToDouble(lector["PrecioConsulta"])
                    };

                    lista.Add(m);
                }
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en GetAll");
            }
            if (m == null)
            {
                throw new Exception("Medico inexistente");
            }

            return lista;
        }

    
        public Medico GetById(int idBuscado)
        {
            Medico m = null;
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"SELECT IdMedico,
                                  Nombre, 
                                  Apellido, 
                                  DNI,
                                  Matricula,
                                  Especialidad,
                                  Telefono,
                                  Email,
                                  PrecioConsulta
                                  FROM Medico 
                                  WHERE IdMedico = @Id;";

            try
            {
                using var comando = new SqliteCommand(sql,conexion);
                comando.Parameters.AddWithValue("@Id", idBuscado);

                using var lector = comando.ExecuteReader();

                if(lector.Read())
                {
                    m = new Medico()
                    {
                        IdMedico = Convert.ToInt32(lector["IdMedico"]),
                        Nombre = Convert.ToString(lector["Nombre"]),
                        Apellido = Convert.ToString(lector["Apellido"]),
                        DNI = Convert.ToString(lector["DNI"]),
                        Matricula = Convert.ToString(lector["Matricula"]),
                        Especialidad = (Especialidad)Enum.Parse(typeof(Especialidad), lector["Especialidad"].ToString()),                        Telefono = Convert.ToString(lector["Telefono"]),
                        Email = Convert.ToString(lector["Email"]),
                        PrecioConsulta = Convert.ToDouble(lector["PrecioConsulta"])
                    };

                    return m; 
                }
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en GetById");
            }
            if (m == null)
            {
                throw new Exception("Paciente inexistente");
            }

            return null;
        }
        public void Add(Medico medico)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"INSERT INTO Medico (Nombre, Apellido, DNI, Matricula, Especialidad, Telefono, Email, PrecioConsulta) 
                           VALUES (@Nombre, @Apellido, @DNI, @Matricula, @Especialidad, @Telefono, @Email, @PrecioConsulta)";


            try
            {
                using var comando = new SqliteCommand(sql,conexion);

                comando.Parameters.Add(new SqliteParameter("@Nombre",medico.Nombre));
                comando.Parameters.Add(new SqliteParameter("@Apellido",medico.Apellido));
                comando.Parameters.Add(new SqliteParameter("@DNI",medico.DNI));
                comando.Parameters.Add(new SqliteParameter("@Matricula",medico.Matricula));
                comando.Parameters.Add(new SqliteParameter("@Especialidad",medico.Especialidad));
                comando.Parameters.Add(new SqliteParameter("@Telefono",medico.Telefono));
                comando.Parameters.Add(new SqliteParameter("@Email",medico.Email));
                comando.Parameters.Add(new SqliteParameter("@PrecioConsulta",medico.PrecioConsulta));

                comando.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos en Add");
            }
        }
        public void Update(Medico medico, int idBuscado)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            string sql = @"UPDATE Medico SET Nombre = @Nombre,
                                               Apellido = @Apellido,
                                               DNI = @DNI, 
                                               Matricula = @Matricula, 
                                               Especialidad = @Especialidad, 
                                               Telefono = @Telefono, 
                                               Email = @Email,
                                               PrecioConsulta = @PrecioConsulta 
                                               WHERE IdMedico = @IdMedico;";

            try
            {
                using var comando = new SqliteCommand(sql,conexion);

                comando.Parameters.Add(new SqliteParameter("@Nombre",medico.Nombre));
                comando.Parameters.Add(new SqliteParameter("@Apellido",medico.Apellido));
                comando.Parameters.Add(new SqliteParameter("@DNI",medico.DNI));
                comando.Parameters.Add(new SqliteParameter("@Matricula",medico.Matricula));
                comando.Parameters.Add(new SqliteParameter("@Especialidad",medico.Especialidad));
                comando.Parameters.Add(new SqliteParameter("@Telefono",medico.Telefono));
                comando.Parameters.Add(new SqliteParameter("@Email",medico.Email));
                comando.Parameters.Add(new SqliteParameter("@PrecioConsulta",medico.PrecioConsulta));
                comando.Parameters.AddWithValue("@IdMedico", idBuscado);

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

            string sql = "DELETE FROM Medico WHERE IdMedico = @IdMedico";

            try
            {
                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.Add(new SqliteParameter("@IdMedico",idBuscado));
                comando.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw new Exception("Error al intentar acceder a la base de datos Delete");
            }
        }
    }
}