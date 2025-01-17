using RiraToDoList.Domain;
using RiraToDoList.Domain.Repository.Interfaces;
using RiraToDoList.Infrastructure.Persistence.Context;
using RiraToDoList.Infrastructure.Persistence.Repository.Implimentations;

namespace RiraToDoList.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ITaskRepository _taskRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
