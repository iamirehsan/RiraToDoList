using RiraToDoList.Domain.Repository.Interfaces;

namespace RiraToDoList.Domain;

public interface IUnitOfWork
{
    public ITaskRepository TaskRepository { get; }
    Task CommitAsync();
    void Commit();
}
