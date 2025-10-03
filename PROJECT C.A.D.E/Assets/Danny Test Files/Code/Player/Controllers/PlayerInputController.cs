using RevolutionStudios.Player.Data;
using RevolutionStudios.Player.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController
{
    private readonly PlayerController playerController;
    private readonly PlayerInputControllerData inputControllerData;

    public Vector2 MoveInput => inputControllerData.MovementInput;
    public Vector2 LookInput => inputControllerData.LookInput;

    public Vector3 MovementDirection => (MoveY * playerController.transform.forward) + (MoveX * playerController.transform.right);

    public float MoveX => inputControllerData.MoveX;
    public float MoveY => inputControllerData.MoveY;

    public float LookX => inputControllerData.LookX;
    public float LookY => inputControllerData.LookY;

    public PlayerInputController(PlayerController player, PlayerInputControllerSettings settings)
    {
        playerController = player;
        inputControllerData = settings.data;
    }

    public void Initialize()
    {
        inputControllerData.InitializeInputMaster();
    }
}
