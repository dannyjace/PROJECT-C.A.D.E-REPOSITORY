using RevolutionStudios.Player.Utilities;
using UnityEngine;

public class PlayerCameraController
{
    private readonly PlayerController playerController;
    private readonly Transform cameraRig;
    private readonly Transform cameraRigTargetTransform;

    private float cameraRigXRotation;
    private float cameraRigZRotation;

    public PlayerCameraController(PlayerController player, PlayerCameraControllerSettings settings)
    {
        playerController = player;  
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
        float moveX = GameManager.instance.InputManager.MoveInput.x;

        float lookX = GameManager.instance.InputManager.LookInput.x;
        float lookY = GameManager.instance.InputManager.LookInput.y * (playerController.CameraSensitivity.y / 2) * Time.deltaTime;

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
