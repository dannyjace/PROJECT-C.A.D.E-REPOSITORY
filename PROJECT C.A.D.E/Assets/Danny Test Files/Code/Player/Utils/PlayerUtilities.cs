using RevolutionStudios.Player.Data;
using UnityEngine;

namespace RevolutionStudios.Player.Utilities
{
    public enum PlayerGroundedState
    {
        Airborne,
        Grounded
    }
    public enum PlayerLocomotionState
    {
        Default = 1,
        Sprinting = 2,
    }
    public enum PlayerAimingState
    {
        Inactive,
        Active
    }


    [System.Serializable]
    public struct PlayerInputControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerInputControllerData data;
    }

    [System.Serializable]
    public struct PlayerMovementControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerMovementControllerData data;
        [Space(20)]

        [Header("REFERENCES")]
        [Space(10)]
        public CharacterController controller;
        [Space(20)]

        [Header("STATES")]
        [Space(10)]
        public PlayerGroundedState groundedState;
        [Space(5)]
        public PlayerLocomotionState locomotionState;
        [Space(5)]
        public PlayerAimingState aimingState;
    }

    [System.Serializable]
    public struct PlayerCameraControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerCameraControllerData data;
        [Space(20)]

        [Header("REFERENCES")]
        [Space(10)]
        public Camera camera;
        [Space(5)]
        public Transform cameraRigTransform;
        [Space(5)]
        public Transform cameraRigTargetTransform;
    }

    [System.Serializable]
    public struct PlayerAnimationControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerAnimationControllerData data;
        [Space(20)]

        [Header("REFERENCES")]
        [Space(10)]
        public Animator animator;
        [Space(10)]
        public Transform animatorLookAtTransform;
        [Space(10)]
        public Transform masterIKTransform;
        [Space(10)]
        public Transform handIKTransform;
        public Transform rightHandIKTransform;
        public Transform leftHandIKTransform;
        [Space(5)]
        public Transform leftHandIKTargetTransform;
        [Space(10)]
        public Transform bobbingPivotTransform;
        public Transform swayPivotTransform;
        [Space(10)]
        public Transform posePositionPivotTransform;
        public Transform poseRotationPivotTransform;
        [Space(10)]
        public Transform weaponRecoilPivotTransform;
    }

    [System.Serializable]
    public struct PlayerAttributeControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerAttributeControllerData data;
    }

    [System.Serializable]
    public struct PlayerAudioControllerSettings
    {
        [Header("CONTROLLER DATA")]
        [Space(10)]
        public PlayerAudioControllerData data;
        [Space(20)]

        [Header("REFERENCES")]
        [Space(10)]
        public AudioSource footStepAudioSource;
    }



    public static class PlayerUtilities { }
}
