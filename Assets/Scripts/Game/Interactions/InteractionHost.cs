using System.Collections.Generic;
using System.Linq;

namespace Runtime
{
    public sealed class InteractionHost: IInteractionHost
    {
        public static IInteractionHost Singleton { get; private set; } = new InteractionHost();

        private Dictionary<IInteractable, object> _interactables = new();

        public void Register(object receiver, IInteractable executor) => _interactables.Add(executor, receiver);

        public void Remove(IInteractable executor) => _interactables.Remove(executor);

        public void Remove(object receiver)
        {
            var relatedInteractables = _interactables.Where(keyValue => keyValue.Value == receiver)
                .Select(keyValue => keyValue.Key);

            foreach (var interactable in relatedInteractables)
                Remove(interactable);
        }

        public void SendInteraction<T>(T sender, object to)
        {
            var suitableInteractables = _interactables.Where(keyValue => keyValue.Value == to)
                .Select(keyValue => keyValue.Key);

            foreach (var interactable in suitableInteractables)
                interactable.Interact(sender);
        }
    }
}
