using System.Text;
using Unity.Netcode;
using UnityEngine;
using ConnectionApprovalRequest = Unity.Netcode.NetworkManager.ConnectionApprovalRequest;
using ConnectionApprovalResponse = Unity.Netcode.NetworkManager.ConnectionApprovalResponse;

namespace Runtime
{
    public sealed class ConnectionDataRecorder : MonoBehaviour
    {
        public static void SetData(string name)
        {
            NetworkManager.Singleton.NetworkConfig.ConnectionData = UTF8Encoding.UTF8.GetBytes(name);
        }

        public static string GetData(byte[] bytes)
        {
            return UTF8Encoding.UTF8.GetString(bytes);
        }

        public void Start()
        {
            NetworkManager.Singleton.OnServerStarted += OnServerStarted;
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            NetworkManager.Singleton.OnClientDisconnectCallback += RemoveConnectionDataById;
        }

        private void RemoveConnectionDataById(ulong id) => ConnectionDataContainer.Singleton.Remove(id);

        private void ConnectionApproval(ConnectionApprovalRequest request, ConnectionApprovalResponse response)
        {
            Debug.Log("Connection approval for " + request.ClientNetworkId);

            if (TryAddConnectionData(request.ClientNetworkId, request.Payload))
            {
                response.Approved = true;
                return;
            }

            response.Approved = false;
        }

        private bool TryAddConnectionData(ulong id, byte[] data)
        {
            var connectionDataContainer = ConnectionDataContainer.Singleton;
            var connectionData = GetData(data);

            if (connectionDataContainer.Contains(id) == false &&
                connectionDataContainer.ContainsData(connectionData) == false)
            {
                connectionDataContainer.Add(id, connectionData);
                return true;
            }

            return false;
        }

        private void OnServerStarted()
        {
            ConnectionDataContainer.Singleton.Clear();

            TryAddConnectionData(0, NetworkManager.Singleton.NetworkConfig.ConnectionData);
        }
    }
}
