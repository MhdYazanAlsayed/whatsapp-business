using SprintBusiness.Shared.Hubs.Enums;

namespace SprintBusiness.Shared.Helpers
{
    public static class SignalrManager
    {
        private static readonly Dictionary<int, HashSet<string>>[] _connections =
            {
                new Dictionary<int, HashSet<string>>() ,
                new Dictionary<int, HashSet<string>>()
            };

        public static void Add(HubType type ,int employeeId, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string>? connections;
                if (!_connections[(int)type].TryGetValue(employeeId, out connections))
                {
                    connections = new HashSet<string>();
                    _connections[(int)type].Add(employeeId, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public static IEnumerable<string> GetConnections(HubType type , int employeeId)
        {
            if (_connections[(int)type].TryGetValue(employeeId, out var connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public static void Remove(HubType type ,int employeeId, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string>? connections;
                if (!_connections[(int)type].TryGetValue(employeeId, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections[(int)type].Remove(employeeId);
                    }
                }
            }
        }
    }
}
