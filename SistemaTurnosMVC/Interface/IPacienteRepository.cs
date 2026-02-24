using SistemaTurnosMVC.Models;

namespace SistemaTurnosMVC.Interface
{
    public interface IPacienteRepository
    {
        public List<Paciente> GetAll();
        public List<Paciente> FiltrarPorSexo(string sexo);
        public Paciente GetById(int idBuscado);
        public void Add(Paciente paciente);
        public void Update(Paciente paciente, int idBuscado);
        public void Delete(int idBuscado);
    }
}