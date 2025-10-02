using UnityEngine;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Attribute Controller Data", menuName = "Revolution Studios/Data/Player/Attribute Controller Data")]
    public class PlayerAttributeControllerData : ScriptableObject
    {
        [Header("Health Properties")]
        [Space(10)]
        [Range(0, 100)] public int currentHealth;
        [Space(5)]
        [Range(0, 100)] public int healthRegenerationRate;
        [Space(20)]

        [Header("Stamina Properties")]
        [Space(10)]
        [Range(0, 100)] public int currentStamina;
        [Space(10)]
        [Range(0, 100)] public int staminaRegenerationRate;
        [Space(5)]
        [Range(0, 100)] public int staminaDegenerationRate;
        [Space(20)]

        [Header("Experience Properties")]
        [Space(10)]
        [Range(0, 100)] public int currentExperience;
        [Space(5)]
        [Range(0, 100000)] public int maximumExperience;
        [Space(20)]

        [Header("Interaction Properties")]
        [Space(10)]
        [Range(0, 100)] public int interactionRange;
        [Space(10)]
        public LayerMask interactionIgnoreLayer;
    }
}
