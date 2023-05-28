using UnityEngine;

namespace Runtime
{
    [System.Serializable]
    internal sealed class PlayerConfiguration
    {
        [field: SerializeField] internal float JumpForce { get; private set; }
        [field: SerializeField] internal float MoveSpeed { get; private set; }
        [field: SerializeField] internal float RotationSpeed { get; private set; }
        [field: SerializeField] internal float InitialHealth { get; private set; }
    }
}
