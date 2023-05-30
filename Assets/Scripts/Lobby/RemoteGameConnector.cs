using System.Net;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System.Net.Sockets;

namespace Runtime
{
    public sealed class RemoteGameConnector: MonoBehaviour
    {
        private async void Start()
        {
            await UnityServices.InitializeAsync();

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async Task<Lobby> CreateLobby(string lobbyName)
        {
            try
            {
                CreateLobbyOptions options = new() {
                    IsPrivate = false,
                    Data = new() {
                        { "Address", new DataObject(DataObject.VisibilityOptions.Public, GetAddress()) }
                    }
                };

                return await LobbyService.Instance.CreateLobbyAsync(lobbyName, 4, options);
            }
            catch(LobbyServiceException ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }

        private string GetAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
