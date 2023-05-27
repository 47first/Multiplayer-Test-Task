using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace Runtime
{
    public class SceneView : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI codeTmp;
        [field: SerializeField] private int MaxConnections { get; set; }

        private void Start()
        {
            SetJoinCode();
        }

        private async void SetJoinCode()
        {
            codeTmp.text = "??????";

            string joinCode = await GetJoinCode(MaxConnections);

            codeTmp.text = joinCode;
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
