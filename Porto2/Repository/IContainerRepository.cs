using Microsoft.EntityFrameworkCore;
using Porto.Models;

namespace Porto.Repositorio
{
    public interface IContainerRepository
    {
        List<Container> ListAll();
        Container Add(Container container);
        Container Edit(Container container);
        Container GetById(int Id);
        Container GetByClientId(int Id);
        void Delete(int Id);
    }
}
