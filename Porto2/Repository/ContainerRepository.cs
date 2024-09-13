using Microsoft.EntityFrameworkCore;
using Porto.Data;
using Porto.Models;

namespace Porto.Repositorio
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly DataBaseContext _context;
        public ContainerRepository(DataBaseContext context)
        {
            _context = context;
        }

        public Container Add(Container container)
        {
            _context.Container.Add(container);
            _context.SaveChanges();

            return container;
        }

        public List<Container> ListAll()
        {
            Container tex = (Container)_context.Container.Where(x => x.Enabled).Include(x => x.Client).FirstOrDefault();

            List<Container> lista = (List<Container>)_context.Container.Where(x => x.Enabled).Include(x => x.Client).ToList();
            return lista;
        }

        public Container GetById(int Id)
        {
            Container container = _context.Container.FirstOrDefault(x => x.Id == Id);
            return container;
        }

        public Container GetByClientId(int Id)
        {
            Container container = _context.Container.FirstOrDefault(x => x.ClientId == Id && x.Enabled);
            return container;
        }

        public Container Edit(Container containerNew)
        {
            Container container = GetById(containerNew.Id);

            if (container == null) throw new Exception("Houve um erro ao encontrar o container");

            container.Number = containerNew.Number;
            container.Type = containerNew.Type;
            container.Status = containerNew.Status;
            container.ClientId = containerNew.ClientId;
            container.Category = containerNew.Category;

            _context.Container.Update(container);
            _context.SaveChanges();

            return containerNew;
        }
        public void Delete(int Id)
        {
            Container container = GetById(Id);
            container.Enabled = false;

            _context.Container.Update(container);
            _context.SaveChanges();

        }
    }
}
