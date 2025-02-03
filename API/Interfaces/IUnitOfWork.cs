namespace API.Interfaces;

public interface IUnitOfWork
{
    ILikesRepository LikesRepository { get; }
    IMessageRepository MessageRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}
