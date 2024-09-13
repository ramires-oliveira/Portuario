using Porto.Data;
using Porto.Models;

namespace Porto.Repositorio
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext _context;
        private readonly IContainerRepository _containerRepository;

        public ClientRepository(DataBaseContext context, IContainerRepository containerRepository)
        {
            _context = context;
            _containerRepository = containerRepository;
        }

        public Client Add(Client cliente)
        {
            _context.Client.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public List<Client> ListAll()
        {
            List<Client> lista = (List<Client>)_context.Client.ToList().Where(x => x.Enabled).ToList();
            return lista;
        }

        public Client GetById(int Id)
        {
            Client cliente = _context.Client.FirstOrDefault(x => x.Id == Id);
            return cliente;
        }

        public Client Edit(Client cliente)
        {
            Client clienteNew = GetById(cliente.Id);

            if (clienteNew == null) throw new Exception("Houve um erro ao editar o cliente");

            clienteNew.Name = cliente.Name;
            clienteNew.CNPJ = cliente.CNPJ;

            _context.Client.Update(clienteNew);
            _context.SaveChanges();

            return cliente;
        }
        public void Delete(int Id)
        {
            Container container = _containerRepository.GetByClientId(Id);

            if (container != null)
            {
                throw new Exception("Não é possível excluir o cliente, pois existem containers vinculados a ele.");
            }

            Client cliente = GetById(Id);
            cliente.Enabled = false;

            _context.Client.Update(cliente);
            _context.SaveChanges();

        }
    }
}
