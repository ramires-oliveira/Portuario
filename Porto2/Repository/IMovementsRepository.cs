using Porto.Models;

namespace Porto.Repositorio
{
    public interface IMovementsRepository
    {
        List<Movements> ListAll();
        Movements Add(Movements Movimentacao);
        Movements Edit(Movements Movimentacao);
        Movements GetById(int Id);
        void Delete(int Id);
    }
}
