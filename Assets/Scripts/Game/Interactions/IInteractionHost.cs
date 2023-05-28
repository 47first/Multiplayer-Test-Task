using UnityEngine;

namespace Runtime
{
    public interface IInteractionHost
    {
        public void Register(GameObject receiver, IInteractable executor);
        public void Remove(IInteractable executor);
        public void Remove(GameObject receiver);
        public void SendInteraction<T>(T sender, GameObject to);
    }
}
