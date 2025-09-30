using System.Linq;
using Player.Utilities;
using UnityEngine;

public class PlayerCameraController
{
    private readonly AdvancedPlayerController playerController;
    private readonly PlayerInputController inputController;
    private readonly Transform cameraRig;
    private readonly Transform cameraRigTargetTransform;

    private float cameraRigXRotation;
    private float cameraRigZRotation;

    public PlayerCameraController(AdvancedPlayerController player, PlayerCameraControllerSettings settings)
    {
        playerController = player;  
        inputController = player.InputController;
        cameraRig = settings.cameraRigTransform;
        cameraRigTargetTransform = settings.cameraRigTargetTransform;
    }

    public void Update()
    {
        UpdateCameraRigRotation();
    }
    public void LateUpdate()
    {
        LateUpdateCameraRigPosition();
    }


    private void UpdateCameraRigRotation()
    {
        float moveX = inputController.MoveX;

        float lookX = inputController.LookX;
        float lookY = inputController.LookY * (playerController.CameraSensitivity.y / 2) * Time.deltaTime;

        cameraRigXRotation += lookY;
        cameraRigXRotation = Mathf.Clamp(cameraRigXRotation, playerController.CameraRotationClamp.x, playerController.CameraRotationClamp.y);

        cameraRigZRotation = Mathf.Lerp(cameraRigZRotation, 0f + -moveX + lookX, Time.deltaTime * 10f);
        cameraRigZRotation = Mathf.Clamp(cameraRigZRotation, -0.5f, 0.5f);

        cameraRig.localRotation = Quaternion.Euler(-cameraRigXRotation, 0f, cameraRigZRotation);
    }
    private void LateUpdateCameraRigPosition()
    {
        cameraRig.position = cameraRigTargetTransform.position;
    }
}
