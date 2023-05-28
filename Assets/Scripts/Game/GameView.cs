using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace Runtime
{
    public class GameView : MonoBehaviour
    {
        [field: SerializeField] private int MaxConnections { get; set; }

        private GamePresenter _presenter;

        private void Start()
        {
            _presenter = new();
        }

        private async void SetJoinCode()
        {
            string joinCode = await GetJoinCode(MaxConnections);
        }

        public static async Task<string> GetJoinCode(int maxConn)
        {
            await UnityServices.InitializeAsync();

            if (AuthenticationService.Instance.IsSignedIn == false)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Allocation allocation = await Unity.Services.Relay.RelayService.Instance.CreateAllocationAsync(maxConn);

            var joinCode = await Unity.Services.Relay.RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            return joinCode;
        }
    }
}
