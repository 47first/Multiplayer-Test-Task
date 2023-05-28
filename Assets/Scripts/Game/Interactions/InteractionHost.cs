using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime
{
    public sealed class InteractionHost: IInteractionHost
    {
        public static IInteractionHost Singleton { get; private set; } = new InteractionHost();

        private Dictionary<IInteractable, GameObject> _interactables = new();

        public void Register(GameObject receiver, IInteractable executor) => _interactables.Add(executor, receiver);

        public void Remove(IInteractable executor) => _interactables.Remove(executor);

        public void Remove(GameObject receiver)
        {
            var relatedInteractables = _interactables.Where(keyValue => keyValue.Value == receiver)
                .Select(keyValue => keyValue.Key);

            foreach (var interactable in relatedInteractables)
                Remove(interactable);
        }

        public void SendInteraction<T>(T sender, GameObject to)
        {
            var suitableInteractables = _interactables.Where(keyValue => keyValue.Value == to)
                .Select(keyValue => keyValue.Key);

            foreach (var interactable in suitableInteractables)
                interactable.Interact(sender);
        }
    }
}
