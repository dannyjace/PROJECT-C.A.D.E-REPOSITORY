using RevolutionStudios.Player.Data;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

public class PlayerMovementController
{
    private readonly PlayerController playerController;
    private readonly PlayerMovementControllerData movementControllerData;
    private readonly CharacterController characterController;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool sprinting;

    public PlayerMovementController(PlayerController player, PlayerMovementControllerSettings settings)
    {
        playerController = player;
        movementControllerData = settings.data;
        characterController = settings.controller;
    }

    public void Update()
    {
        UpdateGroundedState();
        UpdateLocomotionState();

        UpdatePlayerMovement();
        UpdatePlayerRotation();
        UpdatePlayerGravity();
    }
    public void DrawGizmos()
    {
        DrawDebugGizmos();
    }

    private void UpdateGroundedState()
    {
        if (Physics.CheckSphere(playerController.transform.position + playerController.GroundCheckRadiusOffset, playerController.GroundCheckRadius, playerController.GroundLayer))
        {
            playerController.GroundedState = PlayerGroundedState.Grounded;
            jumpCount = 0;
        }
        else
        {
            playerController.GroundedState = PlayerGroundedState.Airborne;
        }
    }
    private void UpdateLocomotionState()
    {
        if (sprinting && GameManager.instance.InputManager.MoveInput.y < 0.5f)
        {
            sprinting = false;
        }

        playerController.LocomotionState = sprinting ? PlayerLocomotionState.Sprinting : PlayerLocomotionState.Default;
    }

    private void UpdatePlayerMovement()
    {
        Vector2 moveInput = GameManager.instance.InputManager.MoveInput;
        Vector3 moveDirection = (moveInput.y * playerController.transform.forward) + (moveInput.x * playerController.transform.right);
        Vector3 movementVector = moveDirection * GetTargetMovementSpeed();

        characterController.Move(movementVector * Time.deltaTime);     
    }
    private void UpdatePlayerRotation()
    {
        Vector2 lookInput = GameManager.instance.InputManager.LookInput;
        float rotation = lookInput.x * (playerController.CameraSensitivity.x * 0.25f) * Time.deltaTime;

        playerController.transform.Rotate(playerController.transform.up * rotation);
    }
    public void UpdatePlayerGravity()
    {
        playerVelocity.y -= playerController.GravityForce * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    private float GetTargetMovementSpeed()
    {
        return playerController.LocomotionState == PlayerLocomotionState.Sprinting ? playerController.SprintMovementSpeed : playerController.DefaultMovementSpeed;
    }

    private void DrawDebugGizmos()
    {
        Gizmos.color = playerController.GroundedState == PlayerGroundedState.Grounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(playerController.transform.position + playerController.GroundCheckRadiusOffset, playerController.GroundCheckRadius);
    }


    public void OnPlayerJump()
    {
        if (jumpCount < playerController.MaxJumpCount)
        {
            playerVelocity.y = playerController.JumpForce;
            jumpCount++;
        }
    }
    public void OnPlayerSprint()
    {
        float moveY = GameManager.instance.InputManager.MoveInput.y;

        if (moveY > 0.25f && playerController.AimingState == PlayerAimingState.Inactive)
        {
            sprinting = !sprinting;
        }
        else
        {
            sprinting = false;
        }
    }
}
