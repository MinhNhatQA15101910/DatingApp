using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IMessageRepository
{
    void AddGroup(Group group);
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Connection?> GetConnectionAsync(string connectionId);
    Task<Group?> GetGroupForConnection(string connectionId);
    Task<Message?> GetMessageAsync(int id);
    Task<Group?> GetMessageGroupAsync(string groupName);
    Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams);
    Task<IEnumerable<MessageDto>> GetMessageThreadAsync(
        string currentUsername,
        string recipientUsername
    );
    void RemoveConnection(Connection connection);
    Task<bool> SaveAllAsync();
}
