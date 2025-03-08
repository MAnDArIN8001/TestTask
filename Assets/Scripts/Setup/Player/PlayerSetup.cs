using UnityEngine;

namespace Setup.Player
{
    [CreateAssetMenu(fileName = "NewPlayerSetup", menuName = "Gameplay/Setups/Player Setup")]
    public class PlayerSetup : ScriptableObject
    {
        [field: SerializeField, Header("Controls")] public float MovementSpeed { get; private set; }
        
        [field: SerializeField, Header("Sensetivity")] public float HorizontalSensitivity { get; private set; }
        [field: SerializeField] public float VerticalSensitivity { get; private set; }
        
        [field: SerializeField, Header("Communication")] public float PickingDistance { get; private set; }
    }
}
