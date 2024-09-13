using Microsoft.EntityFrameworkCore;
using Porto.Data;
using Porto.Models;

namespace Porto.Repositorio
{
    public class MovementsRepository : IMovementsRepository
    {
        private readonly DataBaseContext _context;
        public MovementsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public Movements Add(Movements Movements)
        {
            _context.Movement.Add(Movements);
            _context.SaveChanges();

            return Movements;
        }

        public List<Movements> ListAll()
        {
            List<Movements> movementsList = _context.Movement
            .Where(x => x.Enabled && x.Container.Client.Enabled)
            .Include(x => x.Container)
            .Include(b => b.Container.Client)
            .ToList();

            return movementsList;
        }

        public Movements GetById(int Id)
        {
            Movements Movement = _context.Movement.FirstOrDefault(x => x.Id == Id);
            return Movement;
        }

        public Movements Edit(Movements MovementNew)
        {
            Movements movement = GetById(MovementNew.Id);

            if (movement == null) throw new Exception("Houve um erro ao editar a movimentação");

            movement.MovementType = MovementNew.MovementType;
            movement.StartDate = MovementNew.StartDate;
            movement.EndDate = MovementNew.EndDate;

            _context.Movement.Update(movement);
            _context.SaveChanges();

            return MovementNew;
        }
        public void Delete(int Id)
        {
            Movements Movement = GetById(Id);
            Movement.Enabled = false;

            _context.Movement.Update(Movement);
            _context.SaveChanges();

        }
    }
}
