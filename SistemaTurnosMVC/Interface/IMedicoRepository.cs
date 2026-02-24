using SistemaTurnosMVC.Models;

namespace SistemaTurnosMVC.Interface
{
    public interface IMedicoRepository
    {
        public List<Medico> GetAll();
        public Medico GetById(int idBuscado);
        public void Add(Medico medico);
        public void Update(Medico medico, int idBuscado);
        public void Delete(int idBuscado);
    }
}