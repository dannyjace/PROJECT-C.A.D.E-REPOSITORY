using RevolutionStudios.Player.Utilities;
using RevolutionStudios.Player.Data;
using UnityEngine;

public class PlayerAnimationController 
{
    private readonly PlayerController playerController;
    private readonly PlayerInputController inputController;
    private readonly PlayerAnimationControllerSettings animationControllerSettings;
    private readonly Animator animator;
    private readonly Transform rightHandIK;
    private readonly Transform leftHandIK;
    private readonly Transform leftHandIKTarget;
    private readonly Transform animatorLookAt;

    private float animatorLookXAngle;

    private Vector3 lookSwayRotationInput;
    private Vector3 lookSwayPositionInput;
    private Vector3 lookSwayRotation;
    private Vector3 lookSwayPosition;

    private Vector3 targetBobbingPivotPosition;
    private Vector3 targetBobbingPivotRotation;
    private float targetBobbingFrequency;
    private float targetBobbingAmplitude;

    private Vector3 targetPosePosition;
    private Vector3 targetPoseRotation;

    public PlayerAnimationController(PlayerController player, PlayerAnimationControllerSettings settings)
    {
        playerController = player;
        inputController = player.InputController;

        animationControllerSettings = settings;
        animator = settings.animator;
        rightHandIK = settings.rightHandIKTransform;
        leftHandIK = settings.leftHandIKTransform;
        leftHandIKTarget = settings.leftHandIKTargetTransform;
        animatorLookAt = settings.animatorLookAtTransform;
    }

    public void Update()
    {
        UpdateAnimator();
        UpdateTargetBobbingValues(inputController.MoveInput, animationControllerSettings.data);
    }
    public void LateUpdate()
    {
        LateUpdateSway(animationControllerSettings.swayPivotTransform, inputController.MoveInput, inputController.LookInput, animationControllerSettings.data);
        LateUpdateBobbing(animationControllerSettings.bobbingPivotTransform, inputController.MoveInput);
        LateUpdatePose(animationControllerSettings.posePositionPivotTransform, animationControllerSettings.poseRotationPivotTransform, animationControllerSettings.data);
        LateUpdateHandIK(leftHandIKTarget);
    }
    public void OnAnimatorIK()
    {
        UpdateAnimatorLookIK(animatorLookAt);
        UpdateAnimatorHandIK(animator, rightHandIK, leftHandIK);
    }


    private void UpdateAnimator()
    {
        animator.SetBool("Grounded", playerController.GroundedState == PlayerGroundedState.Grounded);

        animator.SetFloat("Movement X", inputController.MoveX, animationControllerSettings.data.animatorMoveSmoothing, Time.deltaTime);
        animator.SetFloat("Movement Y", inputController.MoveY, animationControllerSettings.data.animatorMoveSmoothing, Time.deltaTime);

        float lookX = inputController.LookX;
        float lookY = inputController.LookY * (playerController.CameraSensitivity.y / 2) * Time.deltaTime;

        animatorLookXAngle += lookY;
        animatorLookXAngle = Mathf.Clamp(animatorLookXAngle, playerController.CameraRotationClamp.x, playerController.CameraRotationClamp.y);

        animator.SetFloat("Look X Angle", animatorLookXAngle, animationControllerSettings.data.animatorLookSmoothing, Time.deltaTime);
        animator.SetFloat("Look Y Angle", lookX, animationControllerSettings.data.animatorLookSmoothing, Time.deltaTime);

        float targetSprintingWeight = playerController.LocomotionState == PlayerLocomotionState.Sprinting ? 1f : 0f;
        animator.SetFloat("Sprinting Weight", targetSprintingWeight, animationControllerSettings.data.animatorSprintWeightSmoothing, Time.deltaTime);
    }
    private void UpdateTargetBobbingValues(Vector2 moveInput, PlayerAnimationControllerData data)
    {
        if (playerController.LocomotionState == PlayerLocomotionState.Sprinting)
        {
            targetBobbingFrequency = Mathf.Lerp(targetBobbingFrequency, data.sprintingBobbingFrequency, moveInput.magnitude * 100);
            targetBobbingAmplitude = Mathf.Lerp(targetBobbingAmplitude, data.sprintingBobbingAmplitude, moveInput.magnitude);
        }
        else
        {
            targetBobbingFrequency = Mathf.Lerp(data.idleBobbingFrequency, data.movingBobbingFrequency, moveInput.magnitude * 100);
            targetBobbingAmplitude = Mathf.Lerp(data.idleBobbingAmplitude, data.movingBobbingAmplitude, moveInput.magnitude);
        }
    }
    private float GetTargetAimingMultiplier()
    {
        if (playerController.GunManager.weaponList.Count > 0)
        {
            return playerController.AimingState == PlayerAimingState.Active ? 0.25f : 1f;
        }
        else
        {
            return 1;
        }
    }

    private void LateUpdateSway(Transform swayPivot, Vector2 moveInput, Vector2 lookInput, PlayerAnimationControllerData data)
    {
        lookSwayRotationInput.x = Mathf.Lerp(lookSwayRotationInput.x, -lookInput.y, Time.deltaTime * data.swaySpeedMultiplier.x);
        lookSwayRotationInput.y = Mathf.Lerp(lookSwayRotationInput.y, -lookInput.x, Time.deltaTime * data.swaySpeedMultiplier.y);
        lookSwayRotationInput.z = Mathf.Lerp(lookSwayRotationInput.z, -lookInput.x, Time.deltaTime * data.swaySpeedMultiplier.z);

        lookSwayRotation.x = (swayPivot.localRotation.x + data.lookSwayRotationMultiplier.x) * lookSwayRotationInput.x * GetTargetAimingMultiplier();
        lookSwayRotation.y = (swayPivot.localRotation.y + data.lookSwayRotationMultiplier.y) * lookSwayRotationInput.y * GetTargetAimingMultiplier();
        lookSwayRotation.z = (swayPivot.localRotation.z + data.lookSwayRotationMultiplier.z) * lookSwayRotationInput.z * GetTargetAimingMultiplier();

        swayPivot.localRotation = Quaternion.Euler(lookSwayRotation);
    }
    private void LateUpdateBobbing(Transform bobbingPivot, Vector2 moveInput)
    {
        if (playerController.GroundedState == PlayerGroundedState.Grounded)
        {
            targetBobbingPivotPosition.y = Mathf.Sin(Time.time * targetBobbingFrequency) * targetBobbingAmplitude / 7500 * GetTargetAimingMultiplier();

            targetBobbingPivotRotation.x = Mathf.Cos(Time.time * -targetBobbingFrequency) * targetBobbingAmplitude / 50 * GetTargetAimingMultiplier();
            targetBobbingPivotRotation.y = Mathf.Cos(Time.time * (targetBobbingFrequency / 2)) * -targetBobbingAmplitude / 50 * GetTargetAimingMultiplier();
        }
        else
        {
            targetBobbingPivotPosition = Vector3.Lerp(targetBobbingPivotPosition, Vector3.zero, Time.deltaTime * 10f);
            targetBobbingPivotRotation = Vector3.Lerp(targetBobbingPivotRotation, Vector3.zero, Time.deltaTime * 10f);
        }

        bobbingPivot.SetLocalPositionAndRotation(Vector3.Lerp(bobbingPivot.localPosition, targetBobbingPivotPosition, Time.deltaTime * 5f), Quaternion.Slerp(bobbingPivot.localRotation, Quaternion.Euler(targetBobbingPivotRotation), Time.deltaTime * 5f));
    }
    private void LateUpdatePose(Transform positionPivot, Transform rotationPivot, PlayerAnimationControllerData data)
    {
        if (playerController.AimingState == PlayerAimingState.Active)
        {
            if (playerController.GunManager.weaponList.Count > 0)
            {
                targetPosePosition = playerController.GunManager.CurrentWeaponData.aimingPosePosition;
                targetPoseRotation = playerController.GunManager.CurrentWeaponData.aimingPoseRotation;
            }
        }
        else
        {
            if (playerController.LocomotionState == PlayerLocomotionState.Default)
            {
                targetPosePosition = data.defaultPosePosition;
                targetPoseRotation = data.defaultPoseRotation;
            }
            else
            {
                targetPosePosition = data.sprintPosePosition;
                targetPoseRotation = data.sprintPoseRotation;
            }
        }

        positionPivot.localPosition = Vector3.Lerp(positionPivot.localPosition, targetPosePosition, Time.deltaTime * data.posePositionSpeed);
        rotationPivot.localRotation = Quaternion.Lerp(rotationPivot.localRotation, Quaternion.Euler(targetPoseRotation), Time.deltaTime * data.poseRotationSpeed);
    }
    private void LateUpdateHandIK(Transform target)
    {
        leftHandIK.SetPositionAndRotation(target.position, target.rotation);
    }


    private void UpdateAnimatorLookIK(Transform lookAtTarget)
    {
        animator.SetLookAtWeight(1f, 0.1f, 1f);
        animator.SetLookAtPosition(lookAtTarget.position);
    }
    private void UpdateAnimatorHandIK(Animator animator, Transform rightHandIK, Transform leftHandIK)
    {
        float rightWeight = animator.GetFloat("Right Hand IK Weight");
        float leftWeight = animator.GetFloat("Left Hand IK Weight");

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightWeight);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIK.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIK.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIK.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIK.rotation);
    }
}
