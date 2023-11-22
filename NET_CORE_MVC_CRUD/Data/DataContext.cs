using Microsoft.EntityFrameworkCore;
using NET_CORE_MVC_CRUD.Models.Domain;

namespace NET_CORE_MVC_CRUD.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
