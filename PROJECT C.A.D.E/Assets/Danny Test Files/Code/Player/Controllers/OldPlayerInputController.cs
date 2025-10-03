using UnityEngine;

public class OldPlayerInputController
{
    private PlayerController playerController;
    private float moveX;
    private float moveY;
    private float lookX;
    private float lookY;
    private Vector3 movementDirection;
    private bool jumpPressed;
    private bool interactHeld;
    private bool sprintHeld;
    private bool aimHeld;
    private bool fireHeld;

    public float MoveX => moveX;
    public float MoveY => moveY;
    public float LookX => lookX;
    public float LookY => lookY;

    public Vector2 MoveInput { get { return new Vector2(moveX, moveY); } }
    public Vector2 LookInput { get { return new Vector2(lookX, lookY); } }
    public Vector3 MovementDirection => movementDirection.normalized;
    public bool JumpPressed => jumpPressed;
    public bool InteractHeld => interactHeld;
    public bool SprintHeld => sprintHeld;   
    public bool AimHeld => aimHeld;
    public bool FireHeld => fireHeld;

    public OldPlayerInputController(PlayerController player)
    {
        playerController = player;
    }

    public void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        lookX = Input.GetAxisRaw("Mouse X") * playerController.CameraSensitivity.x * Time.deltaTime;
        lookY = Input.GetAxisRaw("Mouse Y") * playerController.CameraSensitivity.y * Time.deltaTime;

        movementDirection = (MoveY * playerController.transform.forward) + (MoveX * playerController.transform.right);

        jumpPressed = Input.GetButtonDown("Jump");

        interactHeld = Input.GetButton("Interact");

        sprintHeld = Input.GetButton("Sprint");

        aimHeld = Input.GetButton("Aim");

        fireHeld = Input.GetButton("Fire1");
    }
}
