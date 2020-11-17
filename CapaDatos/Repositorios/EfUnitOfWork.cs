using CapaDominio.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Repositorios
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EfUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
