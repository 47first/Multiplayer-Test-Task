using UnityEngine;

namespace Runtime
{
    [System.Serializable]
    internal sealed class PlayerConfiguration
    {
        [field: SerializeField] internal float JumpForce { get; private set; }
        [field: SerializeField] internal float MoveSpeed { get; private set; }
    }
}
