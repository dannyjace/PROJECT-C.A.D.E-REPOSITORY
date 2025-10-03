using UnityEngine;
using RevolutionStudios.Player.Utilities;
using RevolutionStudios.Player.Data;

public class PlayerMovementController
{
    private readonly PlayerController playerController;
    private readonly PlayerInputController inputController;
    private readonly PlayerMovementControllerData movementControllerData;
    private readonly CharacterController characterController;
    private Vector3 playerVelocity;
    private int jumpCount;

    public PlayerMovementController(PlayerController player, PlayerMovementControllerSettings settings)
    {
        playerController = player;
        inputController = player.InputController;
        movementControllerData = settings.data;
        characterController = settings.controller;
    }

    public void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerRotation();
        UpdatePlayerJump();
        UpdatePlayerGravity();
    }
    public void DrawGizmos()
    {
        DrawDebugGizmos();
    }

    public PlayerGroundedState GetGroundedState()
    {
        if (Physics.CheckSphere(playerController.transform.position + playerController.GroundCheckRadiusOffset, playerController.GroundCheckRadius, playerController.GroundLayer))
        {
            return PlayerGroundedState.Grounded;
        }
        else
        {
            return PlayerGroundedState.Airborne;
        }
    }
    public PlayerLocomotionState GetLocomotionState()
    {
        return PlayerLocomotionState.Default;
    }
    public PlayerAimingState GetAimingState()
    {
        return PlayerAimingState.Inactive;
    }

    private void UpdatePlayerMovement()
    {
        Vector3 movementVector = inputController.MovementDirection * GetTargetMovementSpeed();

        characterController.Move(movementVector * Time.deltaTime);     
    }
    private void UpdatePlayerRotation()
    {
        float rotation = inputController.LookX * (playerController.CameraSensitivity.x * 0.25f) * Time.deltaTime;

        playerController.transform.Rotate(playerController.transform.up * rotation);
    }
    private void UpdatePlayerJump()
    {
        /*

        if (playerController.GroundedState == PlayerGroundedState.Grounded)
        {
            jumpCount = 0;
        }

        if (inputController.JumpPressed && jumpCount < playerController.MaxJumpCount)
        {
            playerVelocity.y = playerController.JumpForce;
            jumpCount++;
        }

        */
    }
    private void UpdatePlayerGravity()
    {
        /*

        if (playerController.GroundedState == PlayerGroundedState.Grounded && !inputController.JumpPressed)
        {
            playerVelocity.y = 0;
        }
        else
        {
            playerVelocity.y -= playerController.GravityForce * Time.deltaTime;

            characterController.Move(playerVelocity * Time.deltaTime);
        }

        */
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
}
