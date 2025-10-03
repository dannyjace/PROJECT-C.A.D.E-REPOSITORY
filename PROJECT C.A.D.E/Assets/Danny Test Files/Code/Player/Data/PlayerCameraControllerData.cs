using UnityEngine;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Camera Controller Data", menuName = "Revolution Studios/Data/Player/Camera Controller Data")]
    public class PlayerCameraControllerData : ScriptableObject
    {
        [Header("Camera Properties")]
        [Space(10)]
        public Vector2 cameraRotationClamp;
        [Space(5)]
        public Vector2 cameraSensitivity;
    }
}