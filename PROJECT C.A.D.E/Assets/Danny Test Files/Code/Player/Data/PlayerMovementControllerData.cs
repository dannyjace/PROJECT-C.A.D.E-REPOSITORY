using UnityEngine;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Movement Controller Data", menuName = "Revolution Studios/Data/Player/Movement Controller Data")]
    public class PlayerMovementControllerData : ScriptableObject
    {
        [Header("Movement Properties")]
        [Space(10)]
        [Range(0, 10f)] public float defaultMovementSpeed;
        [Space(5)]
        [Range(0, 10f)] public float sprintMovementSpeed;
        [Space(10)]
        [Range(0, 100f)] public float movementSpeedSmoothing;
        [Space(20)]

        [Header("Gravity Properties")]
        [Space(10)]
        [Range(0, 25f)] public float gravityForce;
        [Space(20)]

        [Header("Jump Properties")]
        [Space(10)]
        [Range(1, 15)] public int jumpForce;
        [Range(1, 5)] public int maxJumpCount;
        [Space(20)]

        [Header("Ground Check Properties")]
        [Space(10)]
        public LayerMask groundLayer;
        [Space(5)]
        public Vector3 groundCheckRadiusOffset;
        [Space(5)]
        [Range(0.01f, 0.5f)] public float groundCheckRadius;
    }
}