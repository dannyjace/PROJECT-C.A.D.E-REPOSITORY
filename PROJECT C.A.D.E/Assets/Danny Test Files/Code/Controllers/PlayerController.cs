using System.Collections;
using System.Collections.Generic;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    #region PRIVATE PROPERTIES

    [SerializeField] private PlayerMovementControllerSettings movementControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerCameraControllerSettings cameraControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAnimationControllerSettings animationControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAttributeControllerSettings attributeControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAudioControllerSettings audioControllerSettings;
    [Space(20)]

    [Header("INVENTORY SETTINGS")]
    [Space(10)]
    public List<inventoryItem> inventory = new List<inventoryItem>();

    #endregion

    #region PROPERTY GETTERS

    public float DefaultMovementSpeed => movementControllerSettings.data.defaultMovementSpeed;
    public float SprintMovementSpeed => movementControllerSettings.data.sprintMovementSpeed;
    public float GravityForce => movementControllerSettings.data.gravityForce;
    public int JumpForce => movementControllerSettings.data.jumpForce;
    public int MaxJumpCount => movementControllerSettings.data.maxJumpCount;
    public float GroundCheckRadius => movementControllerSettings.data.groundCheckRadius;
    public Vector3 GroundCheckRadiusOffset => movementControllerSettings.data.groundCheckRadiusOffset;
    public LayerMask GroundLayer => movementControllerSettings.data.groundLayer;

    public PlayerGroundedState GroundedState { get { return movementControllerSettings.groundedState; } set { movementControllerSettings.groundedState = value; } }
    public PlayerLocomotionState LocomotionState { get { return movementControllerSettings.locomotionState; } set { movementControllerSettings.locomotionState = value; } }
    public PlayerAimingState AimingState { get { return movementControllerSettings.aimingState; } set { movementControllerSettings.aimingState = value; } }

    public Camera PlayerCamera => cameraControllerSettings.camera;
    public Transform CameraRig => cameraControllerSettings.cameraRigTransform;
    public Vector2 CameraRotationClamp => cameraControllerSettings.data.cameraRotationClamp;
    public Vector2 CameraSensitivity => cameraControllerSettings.data.cameraSensitivity * 100;

    public Transform MasterIK => animationControllerSettings.masterIKTransform;
    public Transform WeaponRecoilPivot => animationControllerSettings.weaponRecoilPivotTransform;

    #endregion

    #region CONTROLLERS

    public PlayerInputController InputController { get; private set; }
    public PlayerMovementController MovementController { get; private set; }
    public PlayerCameraController CameraController { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }
    public PlayerAttributeController AttributeController { get; private set; }
    public PlayerAudioController AudioController { get; private set; }


    #endregion

    #region METHODS

    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void InitializeControllers()
    {
        InputController = new PlayerInputController(this);
        MovementController = new PlayerMovementController(this, movementControllerSettings);
        CameraController = new PlayerCameraController(this, cameraControllerSettings);
        AnimationController = new PlayerAnimationController(this, animationControllerSettings);
        AttributeController = new PlayerAttributeController(this, attributeControllerSettings);
        AudioController = new PlayerAudioController(this, audioControllerSettings);

        AttributeController?.Initialize();
    }
    private void UpdateControllers()
    {
        InputController?.Update();
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




    public void TakeDamage(int amount)
    {
        AttributeController.TakeDamage(amount);
    }
    public IEnumerator FlashDamageScreen()
    {
        HUDManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        HUDManager.instance.playerDamageScreen.SetActive(false);
    }


    public bool HasItem(inventoryItem item)
    {
        return inventory.Contains(item);
    }
    public void AddItem(inventoryItem item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
            Debug.Log("Item added to inventory");
        }
    }

    public void SpawnPlayer()
    {
        transform.position = GameManager.instance.playerSpawnPos.transform.position;
    }

    public void FootStep()
    {
        AudioController.PlayFootStepAudio();
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Start()
    {
        InitializeCursor();
        InitializeControllers();
    }

    private void Update()
    {
        UpdateControllers();
    }

    private void LateUpdate()
    {
        LateUpdateControllers();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        AnimationController?.OnAnimatorIK();
    }

    private void OnDrawGizmos()
    {
        MovementController?.DrawGizmos();
    }

    #endregion
}
