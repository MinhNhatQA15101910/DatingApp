using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;

namespace API.Data;

public class MessageRepository(DataContext context, IMapper mapper) : IMessageRepository
{
    public void AddGroup(Group group)
    {
        context.Groups.Add(group);
    }

    public void AddMessage(Message message)
    {
        context.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        context.Messages.Remove(message);
    }

    public async Task<Connection?> GetConnectionAsync(string connectionId)
    {
        return await context.Connections.FindAsync(connectionId);
    }

    public async Task<Message?> GetMessageAsync(int id)
    {
        return await context.Messages.FindAsync(id);
    }

    public async Task<Group?> GetMessageGroup(string groupName)
    {
        return await context.Groups
            .Include(x => x.Connections)
            .FirstOrDefaultAsync(x => x.Name == groupName);
    }

    public async Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams)
    {
        var query = context.Messages
            .OrderByDescending(m => m.MessageSent)
            .AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u =>
                u.Recipient.UserName == messageParams.Username &&
                !u.RecipientDeleted
            ),
            "Outbox" => query.Where(u =>
                u.Sender.UserName == messageParams.Username &&
                !u.SenderDeleted
            ),
            _ => query.Where(
                u => u.Recipient.UserName == messageParams.Username &&
                u.DateRead == null &&
                !u.RecipientDeleted
            )
        };

        var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

        return await PagedList<MessageDto>.CreateAsync(
            messages,
            messageParams.PageNumber,
            messageParams.PageSize
        );
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync(
        string currentUsername,
        string recipientUsername
    )
    {
        var messages = await context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Photos)
            .Include(u => u.Recipient).ThenInclude(p => p.Photos)
            .Where(m =>
                (m.RecipientUsername == currentUsername &&
                    !m.RecipientDeleted &&
                    m.SenderUsername == recipientUsername) ||
                (m.SenderUsername == currentUsername &&
                    !m.SenderDeleted &&
                    m.RecipientUsername == recipientUsername)
            )
            .OrderBy(m => m.MessageSent)
            .ToListAsync();

        var unreadMessages = messages.Where(m =>
            m.DateRead == null &&
            m.Recipient.UserName == currentUsername
        ).ToList();
        if (unreadMessages.Count != 0)
        {
            unreadMessages.ForEach(m => m.DateRead = DateTime.UtcNow);
            await context.SaveChangesAsync();
        }

        return mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public void RemoveConnection(Connection connection)
    {
        context.Connections.Remove(connection);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
