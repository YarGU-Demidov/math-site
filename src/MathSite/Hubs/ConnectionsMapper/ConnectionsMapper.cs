using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MathSite.Hubs.ConnectionsMapper
{
    public class ConnectionsMapper<T>
    {
        private readonly ConcurrentDictionary<T, HashSet<string>> _connections = new ConcurrentDictionary<T, HashSet<string>>();

        public int Count => _connections.Count;

        public void Add(T userId, string connectionId)
        {
            if (!_connections.TryGetValue(userId, out var connections))
            {
                connections = new HashSet<string>();
                _connections.TryAdd(userId, connections);
            }

            connections.Add(connectionId);

        }

        public void Remove(T userId, string connectionId)
        {
            if (!_connections.TryGetValue(userId, out var connections)) return;
            connections.Remove(connectionId);

            if (connections.Count != 0) return;
            _connections.TryRemove(userId, out connections);
        }

        public IEnumerable<string> GetConnections(T userId)
        {
            return !_connections.TryGetValue(userId, out var connections) ? Enumerable.Empty<string>() : connections;        
        }

        public bool IsUserOnline(T userId)
        {
            return GetConnections(userId).Any();
        }
    }
}
