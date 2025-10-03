using System.Collections;
using System.Collections.Generic;
using RevolutionStudios.Player.StateMachine;
using RevolutionStudios.Player.Utilities;
using RevolutionStudios.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] private PlayerInputControllerSettings inputControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerMovementControllerSettings movementControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerCameraControllerSettings cameraControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAnimationControllerSettings animationControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAttributeControllerSettings attributeControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAudioControllerSettings audioControllerSettings;


    public PlayerInputController InputController { get; private set; }
    public PlayerMovementController MovementController { get; private set; }
    public PlayerCameraController CameraController { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public PlayerAttributeController AttributeController { get; private set; }
    public PlayerAudioController AudioController { get; private set; }


    public StateMachine<PlayerController> GroundedStateMachine { get; private set; }
    public StateMachine<PlayerController> LocomotionStateMachine { get; private set; }
    public StateMachine<PlayerController> StanceStateMachine { get; private set; }
    public StateMachine<PlayerController> AimingStateMachine { get; private set; }


    public PlayerGroundedState GroundedState { get { return movementControllerSettings.groundedState; } set { movementControllerSettings.groundedState = value; } }
    public PlayerLocomotionState LocomotionState { get { return movementControllerSettings.locomotionState; } set { movementControllerSettings.locomotionState = value; } }
    public PlayerStanceState StanceState { get { return movementControllerSettings.stanceState; } set { movementControllerSettings.stanceState = value; } }
    public PlayerAimingState AimingState { get { return movementControllerSettings.aimingState; } set { movementControllerSettings.aimingState = value; } }


    public float DefaultMovementSpeed => movementControllerSettings.data.defaultMovementSpeed;
    public float SprintMovementSpeed => movementControllerSettings.data.sprintMovementSpeed;
    public float GravityForce => movementControllerSettings.data.gravityForce;
    public int JumpForce => movementControllerSettings.data.jumpForce;
    public int MaxJumpCount => movementControllerSettings.data.maxJumpCount;
    public float GroundCheckRadius => movementControllerSettings.data.groundCheckRadius;
    public Vector3 GroundCheckRadiusOffset => movementControllerSettings.data.groundCheckRadiusOffset;
    public LayerMask GroundLayer => movementControllerSettings.data.groundLayer;

    public Camera PlayerCamera => cameraControllerSettings.camera;
    public Transform CameraRig => cameraControllerSettings.cameraRigTransform;
    public Vector2 CameraRotationClamp => cameraControllerSettings.data.cameraRotationClamp;
    public Vector2 CameraSensitivity => cameraControllerSettings.data.cameraSensitivity * 100;

    public Transform MasterIK => animationControllerSettings.masterIKTransform;
    public Transform WeaponRecoilPivot => animationControllerSettings.weaponRecoilPivotTransform;


    private void InitializeControllers()
    {
        InputController = new PlayerInputController(this, inputControllerSettings);
        MovementController = new PlayerMovementController(this, movementControllerSettings);
        CameraController = new PlayerCameraController(this, cameraControllerSettings);
        AnimationController = new PlayerAnimationController(this, animationControllerSettings);
        AttributeController = new PlayerAttributeController(this, attributeControllerSettings);
        AudioController = new PlayerAudioController(this, audioControllerSettings);

        InputController?.Initialize();
        AttributeController?.Initialize();
    }
    private void UpdateControllers()
    {
        MovementController?.Update();
        CameraController?.Update();
        AnimationController?.Update();
        AttributeController?.Update();
    }
    private void LateUpdateControllers()
    {
        CameraController?.LateUpdate();
        AnimationController?.LateUpdate();
    }


    private void InitializeStateMachines()
    {
        GroundedStateMachine = new StateMachine<PlayerController>(this);
        LocomotionStateMachine = new StateMachine<PlayerController>(this);
        StanceStateMachine = new StateMachine<PlayerController>(this);
        AimingStateMachine = new StateMachine<PlayerController>(this);

        LocomotionStateMachine.AddState(new PlayerDefaultLocomotionState());
        LocomotionStateMachine.AddState(new PlayerSprintingLocomotionState());
        StanceStateMachine.AddState(new PlayerStandingStanceState());
        StanceStateMachine.AddState(new PlayerCrouchingStanceState());
        StanceStateMachine.AddState(new PlayerProneStanceState());
        AimingStateMachine.AddState(new PlayerInactiveAimingState());
        AimingStateMachine.AddState(new PlayerActiveAimingState());

        LocomotionStateMachine.ChangeState<PlayerDefaultLocomotionState>();
        StanceStateMachine.ChangeState<PlayerStandingStanceState>();
        AimingStateMachine.ChangeState<PlayerInactiveAimingState>();
    }
    private void UpdateStateMachines()
    {
        LocomotionStateMachine?.Update();
        StanceStateMachine?.Update();
    }
    private void LateUpdateStateMachines()
    {
        LocomotionStateMachine.LateUpdate();
        StanceStateMachine?.LateUpdate();
    }
    private void FixedUpdateStateMachines()
    {
        LocomotionStateMachine.FixedUpdate();
        StanceStateMachine?.FixedUpdate();
    }


    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TakeDamage(int amount)
    {
        AttributeController.TakeDamage(amount);
    }
    public IEnumerator FlashDamageScreen()
    {       
        yield return new WaitForSeconds(0.1f);       
    }
    public void SpawnPlayer()
    {
        transform.position = GameManager.instance.playerSpawnPos.transform.position;
    }
    public void FootStep()
    {
        AudioController.PlayFootStepAudio();
    }


    private void Start()
    {
        InitializeCursor();
        InitializeControllers();
        InitializeStateMachines();
    }
    private void Update()
    {
        UpdateControllers();
        UpdateStateMachines();
    }
    private void LateUpdate()
    {
        LateUpdateControllers();
        LateUpdateStateMachines();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        AnimationController?.OnAnimatorIK();
    }
    private void OnDrawGizmos()
    {
        MovementController?.DrawGizmos();
    }
}
