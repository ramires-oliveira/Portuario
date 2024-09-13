using Microsoft.EntityFrameworkCore;
using Porto.Models;
using System.Collections.Generic;

namespace Porto.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Container> Container { get; set; }
        public DbSet<Movements> Movement { get; set; }

    }
}
