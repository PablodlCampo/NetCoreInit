using CapaDatos.Contextos;
using CapaDominio.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Repositorios
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly GlobalContext _context;

        public EfUnitOfWork(GlobalContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
