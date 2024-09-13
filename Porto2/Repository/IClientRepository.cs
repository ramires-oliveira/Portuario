using Porto.Models;

namespace Porto.Repositorio
{
    public interface IClientRepository
    {
        List<Client> ListAll();
        Client Add(Client cliente);
        Client Edit(Client cliente);
        Client GetById(int Id);
        void Delete(int Id);
    }
}
