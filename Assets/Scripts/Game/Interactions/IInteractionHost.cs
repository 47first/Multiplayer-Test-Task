using UnityEngine;

namespace Runtime
{
    public interface IInteractionHost
    {
        public void Register(object receiver, IInteractable executor);
        public void Remove(IInteractable executor);
        public void Remove(object receiver);
        public void SendInteraction<T>(T sender, object to);
    }
}
