using System.Collections.Generic;

namespace Runtime
{
    public sealed class ConnectionDataContainer
    {
        public static ConnectionDataContainer Singleton { get; private set; } = new();

        private Dictionary<ulong, string> _connectionData = new();

        public void Clear() => _connectionData.Clear();

        public bool TryGetData(ulong id, out string data) => _connectionData.TryGetValue(id, out data);

        public void Add(ulong id, string data) => _connectionData.Add(id, data);

        public void Remove(ulong id) => _connectionData.Remove(id);

        public bool Contains(ulong id) => _connectionData.ContainsKey(id);

        public bool ContainsData(string data) => _connectionData.ContainsValue(data);
    }
}
