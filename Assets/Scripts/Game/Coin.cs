using Unity.Netcode;

namespace Runtime
{
    public class Coin : NetworkBehaviour, IInteractable
    {
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
                InteractionHost.Singleton.Register(gameObject, this);
        }

        public void Interact(object sender)
        {
            if (sender is PlayerView playerView)
            {
                playerView.CoinsAmount.Value++;
                NetworkObject.Despawn();
            }
        }
    }
}
