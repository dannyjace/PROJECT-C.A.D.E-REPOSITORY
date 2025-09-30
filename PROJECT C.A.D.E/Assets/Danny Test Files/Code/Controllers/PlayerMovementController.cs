using UnityEngine;
using Player.Utilities;
using Player.Data;

public class PlayerMovementController
{
    private readonly AdvancedPlayerController playerController;
    private readonly PlayerInputController inputController;
    private readonly PlayerMovementControllerData movementControllerData;
    private readonly CharacterController characterController;
    private Vector3 playerVelocity;
    private int jumpCount;
    private bool canSprint;

    public PlayerMovementController(AdvancedPlayerController player, PlayerMovementControllerSettings settings)
    {
        playerController = player;
        inputController = player.InputController;
        movementControllerData = settings.data;
        characterController = settings.controller;
    }

    public void Update()
    {
        UpdateCanSprint();

        UpdatePlayerGroundedState();
        UpdatePlayerLocomotionState();
        UpdatePlayerAimingState();

        UpdatePlayerMovement();
        UpdatePlayerRotation();
        UpdatePlayerJump();
        UpdatePlayerGravity();
    }
    public void DrawGizmos()
    {
        DrawDebugGizmos();
    }

    private void UpdatePlayerGroundedState()
    {
        if (Physics.CheckSphere(playerController.transform.position + playerController.GroundCheckRadiusOffset, playerController.GroundCheckRadius, playerController.GroundLayer))
        {
            playerController.GroundedState = PlayerGroundedState.Grounded;
        }
        else
        {
            playerController.GroundedState = PlayerGroundedState.Airborne;
        }
    }
    private void UpdatePlayerLocomotionState()
    {
        if (playerController.GroundedState == PlayerGroundedState.Grounded && inputController.SprintHeld && canSprint)
        {
            playerController.LocomotionState = PlayerLocomotionState.Sprinting;
        }
        else
        {
            playerController.LocomotionState = PlayerLocomotionState.Default;
        }
    }
    private void UpdatePlayerAimingState()
    {
        if (inputController.AimHeld && playerController.GunManager.weaponList.Count > 0)
        {
            playerController.AimingState = PlayerAimingState.Active;
            HUDManager.instance.Reticle.SetActive(false);
        }
        else
        {
            playerController.AimingState = PlayerAimingState.Inactive;
            HUDManager.instance.Reticle.SetActive(true);
        }
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
        if (playerController.GroundedState == PlayerGroundedState.Grounded)
        {
            jumpCount = 0;
        }

        if (inputController.JumpPressed && jumpCount < playerController.MaxJumpCount)
        {
            playerVelocity.y = playerController.JumpForce;
            jumpCount++;
        }
    }
    private void UpdatePlayerGravity()
    {
        if (playerController.GroundedState == PlayerGroundedState.Grounded && !inputController.JumpPressed)
        {
            playerVelocity.y = 0;
        }
        else
        {
            playerVelocity.y -= playerController.GravityForce * Time.deltaTime;

            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }
    private float GetTargetMovementSpeed()
    {
        return playerController.LocomotionState == PlayerLocomotionState.Sprinting ? playerController.SprintMovementSpeed : playerController.DefaultMovementSpeed;
    }
    private void UpdateCanSprint()
    {
        if (playerController.Stamina >= playerController.InitialStamina && playerController.AimingState == PlayerAimingState.Inactive)
        {
            canSprint = true;
        }
        
        if (playerController.Stamina < 1 || playerController.AimingState == PlayerAimingState.Active)
        {
            canSprint = false;
        }
    }

    private void DrawDebugGizmos()
    {
        Gizmos.color = playerController.GroundedState == PlayerGroundedState.Grounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(playerController.transform.position + playerController.GroundCheckRadiusOffset, playerController.GroundCheckRadius);
    }
}
