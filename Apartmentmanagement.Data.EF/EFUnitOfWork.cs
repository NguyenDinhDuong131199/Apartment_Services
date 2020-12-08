using ApartmentManagement.Infrastructure.Interfaces;

namespace ApartmentManagement.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApartmentManagementDbContext _context;

        public EFUnitOfWork(ApartmentManagementDbContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
