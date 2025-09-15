using SprintBusiness.Domain.Conversations.Keys;

namespace SprintBusiness.Shared.Helpers
{
    public static class ConversationManager
    {
        private static readonly object _lock = new();

        private static readonly Dictionary<ConversationId, HashSet<int>> _conversationsOnline = new();

        private static readonly Dictionary<int, ConversationId> _employeeConnectionId = new();

        public static void AddUser(ConversationId conversationId, int employeeId)
        {
            lock (_lock)
            {
                Remove(employeeId);

                if (!_conversationsOnline.TryGetValue(conversationId, out var employeeIds))
                {
                    employeeIds = new HashSet<int>();
                    _conversationsOnline[conversationId] = employeeIds;
                }

                employeeIds.Add(employeeId);
                _employeeConnectionId[employeeId] = conversationId;
            }
        }

        public static List<int> GetEmployeesIds(ConversationId conversationId)
        {
            lock (_lock)
            {
                if (_conversationsOnline.TryGetValue(conversationId, out var employeeIds))
                {
                    return employeeIds.ToList();
                }

                return new List<int>();
            }
        }

        public static void Remove(int employeeId)
        {
            lock (_lock)
            {
                if (_employeeConnectionId.TryGetValue(employeeId, out var conversationId))
                {
                    if (_conversationsOnline.TryGetValue(conversationId, out var employeeIds))
                    {
                        employeeIds.Remove(employeeId);

                        if (employeeIds.Count == 0)
                        {
                            _conversationsOnline.Remove(conversationId);
                        }
                    }

                    _employeeConnectionId.Remove(employeeId);
                }
            }
        }
    }    
}
