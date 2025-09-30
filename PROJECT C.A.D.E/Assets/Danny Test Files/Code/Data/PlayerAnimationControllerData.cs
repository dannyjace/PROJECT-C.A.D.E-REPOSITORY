using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "New Player Animation Controller Data", menuName = "Player/Data/Animation Controller Data")]
    public class PlayerAnimationControllerData : ScriptableObject
    {
        [Header("Animator Properties")]
        [Space(10)]
        [Range(0, 1f)] public float animatorMoveSmoothing;
        [Space(5)]
        [Range(0, 1f)] public float animatorLookSmoothing;
        [Space(10)]
        [Range(0, 1f)] public float animatorSprintWeightSmoothing;
        [Space(20)]

        [Header("Procedural Bobbing Properties")]
        [Space(10)]
        [Range(0, 5f)] public float idleBobbingFrequency;
        [Range(0, 100f)] public float idleBobbingAmplitude;
        [Space(5)]
        [Range(0, 15f)] public float movingBobbingFrequency;
        [Range(0, 250f)] public float movingBobbingAmplitude;
        [Space(5)]
        [Range(0, 25f)] public float sprintingBobbingFrequency;
        [Range(0, 250f)] public float sprintingBobbingAmplitude;
        [Space(20)]

        [Header("Procedural Sway Properties")]
        [Space(10)]
        public Vector3 swaySpeedMultiplier;
        [Space(10)]
        public Vector3 lookSwayRotationMultiplier;
        public Vector3 lookSwayPositionMultiplier;
        [Space(20)]

        [Header("Procedural Pose Properties")]
        [Space(10)]
        [Range(0, 15f)] public float posePositionSpeed;
        [Range(0, 15f)] public float poseRotationSpeed;
        [Space(10)]
        public Vector3 defaultPosePosition;
        public Vector3 defaultPoseRotation;
        [Space(5)]
        public Vector3 sprintPosePosition;
        public Vector3 sprintPoseRotation;
    }
}
