using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "New Player Camera Controller Data", menuName = "Player/Data/Camera Controller Data")]
    public class PlayerCameraControllerData : ScriptableObject
    {
        [Header("Camera Properties")]
        [Space(10)]
        public Vector2 cameraRotationClamp;
        [Space(5)]
        public Vector2 cameraSensitivity;
    }
}