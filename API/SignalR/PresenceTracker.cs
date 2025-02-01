namespace API.SignalR;

public class PresenceTracker
{
    private static readonly Dictionary<string, List<string>> OnlineUsers = [];

    public Task UserConnectedAsync(string username, string connectionId)
    {
        lock (OnlineUsers)
        {
            if (OnlineUsers.TryGetValue(username, out List<string>? value))
            {
                value.Add(connectionId);
            }
            else
            {
                OnlineUsers.Add(username, [connectionId]);
            }
        }

        return Task.CompletedTask;
    }

    public Task UserDisconnectedAsync(string username, string connectionId)
    {
        lock (OnlineUsers)
        {
            if (!OnlineUsers.TryGetValue(username, out List<string>? value)) return Task.CompletedTask;
            value.Remove(connectionId);

            if (value.Count == 0)
            {
                OnlineUsers.Remove(username);
            }
        }

        return Task.CompletedTask;
    }

    public Task<string[]> GetOnlineUsersAsync()
    {
        string[] onlineUsers;
        lock (OnlineUsers)
        {
            onlineUsers = [.. OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key)];
        }

        return Task.FromResult(onlineUsers);
    }

    public static Task<List<string>> GetConnectionsForUser(string username)
    {
        List<string> connectionIds;

        if (OnlineUsers.TryGetValue(username, out var connections))
        {
            lock (connections)
            {
                connectionIds = [.. connections];
            }
        }
        else
        {
            connectionIds = [];
        }

        return Task.FromResult(connectionIds);
    }
}
