using API.Interfaces;

namespace API.Data;

public class UnitOfWork(
    DataContext context,
    ILikesRepository likesRepository,
    IMessageRepository messageRepository,
    IUserRepository userRepository
) : IUnitOfWork
{
    public ILikesRepository LikesRepository => likesRepository;
    public IMessageRepository MessageRepository => messageRepository;
    public IUserRepository UserRepository => userRepository;

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
}
