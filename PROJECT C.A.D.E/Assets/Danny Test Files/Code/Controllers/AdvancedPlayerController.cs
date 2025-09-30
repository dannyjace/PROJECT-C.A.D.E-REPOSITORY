using System.Collections;
using System.Collections.Generic;
using Player.Utilities;
using UnityEngine;

public class AdvancedPlayerController : MonoBehaviour, IDamage
{
    #region PRIVATE PROPERTIES

    [SerializeField] private PlayerMovementControllerSettings movementControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerCameraControllerSettings cameraControllerSettings;
    [Space(10)]
    [SerializeField] private PlayerAnimationControllerSettings animationControllerSettings;


    [Space(20)]
    [Header("Health Properties")]
    [Space(10)]
    [SerializeField, Range(5, 300)] private int HP;
    [Space(10)]
    [SerializeField, Range(1, 10)] private int healthRegen;
    [Space(20)]

    [Header("Stamina SETTINGS")]
    [Space(10)]
    [SerializeField, Range(0, 100)] private float stamina;
    [Space(10)]

    [Header("Stamina USAGE")]
    [Space(10)]
    [SerializeField, Range(0, 10)] private float staminaUsage;
    [Space(10)]

    [Header("Stamina REGEN")]
    [Space(10)]
    [SerializeField, Range(0, 10)] private float staminaRegen;
    [Space(10)]

    [Header("Starting EXP")]
    [Space(10)]
    [SerializeField, Range(0, 499)] private int startingEXP;
    [Space(10)]

    [Header("INTERACTION SETTINGS")]
    [Space(10)]
    [SerializeField] private LayerMask interactionIgnoreLayer;
    [SerializeField, Range(0, 10)] private float interactRange;

    [Header("GUN SETTINGS")]
    [Space(10)]
    [SerializeField] GunManager gunManager;
    [Space(20)]

    [Header("INVENTORY SETTINGS")]
    [Space(10)]
    public List<inventoryItem> inventory = new List<inventoryItem>();

    [Header("AUDIO SETTINGS")]
    [Space(10)]
    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip  gunFire;
    


    private int initialHP;
    private float initialStamina;
    private int maxEXP;

    private int jumpCount;
    private float shootTimer;
    private int gunListPos;
    private bool canSprint;
    private bool isReloading;
    private AudioSource audioSource;

    public bool isFullyHealed => HP >= initialHP;
    public bool isLowHealth => HP <= initialHP * 0.3f;

    #endregion

    #region PROPERTY GETTERS

    public float DefaultMovementSpeed => movementControllerSettings.data.defaultMovementSpeed;
    public float SprintMovementSpeed => movementControllerSettings.data.sprintMovementSpeed;
    public float Stamina => stamina;
    public float InitialStamina => initialStamina;
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

    public GunManager GunManager => gunManager;

    #endregion

    #region CONTROLLERS

    public PlayerInputController InputController { get; private set; }
    public PlayerMovementController MovementController { get; private set; }
    public PlayerCameraController CameraController { get; private set; }
    public PlayerAnimationController AnimationController { get; private set; }


    #endregion

    #region METHODS

    private void InitializeControllers()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputController = new PlayerInputController(this);
        MovementController = new PlayerMovementController(this, movementControllerSettings);
        CameraController = new PlayerCameraController(this, cameraControllerSettings);
        AnimationController = new PlayerAnimationController(this, animationControllerSettings);

        gunManager = GetComponent<GunManager>();    
    }
    private void UpdateControllers()
    {
        InputController?.Update();
        MovementController?.Update();
        CameraController?.Update();
        AnimationController?.Update();
    }
    private void LateUpdateControllers()
    {
        CameraController?.LateUpdate();
        AnimationController?.LateUpdate();
    }

    public void UpdatePlayerHealthBarUI()
    {
        // updating the player health bar fill to reflect the current HP //
        HUDManager.instance.playerHPBar.fillAmount = (float)HP / initialHP;
    }
    public void FillPlayerHPBar(int healAmount)
    {
        HP += healAmount * healthRegen;
        UpdatePlayerHealthBarUI();

        // Lerp the health bar fill amount to the new HP value

        if (HP > initialHP)
        {
            HP = initialHP;
            UpdatePlayerHealthBarUI();
        }
    }


    public void UpdatePlayerStaminaBarUI()
    {
        // updating the player stamina bar to show the current stamina at game start
        HUDManager.instance.playerStaminaBar.fillAmount = (float)stamina / initialStamina;
    }
    public void UpdateStamina()
    {
        if (LocomotionState == PlayerLocomotionState.Sprinting && stamina > 0)
        {
            stamina -= staminaUsage * Time.deltaTime;
        }
        else if (LocomotionState == PlayerLocomotionState.Default && stamina < initialStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
        }
    }


    public void UpdatePlayerEXPBarUI()
    {
        HUDManager.instance.playerEXPBar.fillAmount = (float)startingEXP / maxEXP;
    }
    public void addEXP(int amount)
    {
        startingEXP += amount;

        if (startingEXP > maxEXP)
        {
            HUDManager.instance.LevelUp();
            startingEXP = startingEXP - maxEXP;
            maxEXP = maxEXP * 2;
        }
    }


    public void TakeDamage(int amount)
    {
        HP -= amount;

        if (isLowHealth && !InfoManager.instance.IsInfoShowing())
        {

            InfoManager.instance.ShowMessage("WARNING!", "Health Critical!", Color.red, 2);
        }

        UpdatePlayerHealthBarUI();
        StartCoroutine(FlashDamageScreen());

        if (HP <= 0)
        {
            GameManager.instance.youLose();
        }
    }
    IEnumerator FlashDamageScreen()
    {
        HUDManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        HUDManager.instance.playerDamageScreen.SetActive(false);
    }


    public void UpdateInteract()
    {
        if (Input.GetButton("Interact"))
        {
            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hit, interactRange, ~interactionIgnoreLayer))
            {
                // logging the collider the raycast hit //
                Debug.Log(hit.collider.name);

                // if the collider has the IDamage interface, we store it in 'target'
                IInteractable target = hit.collider.GetComponent<IInteractable>();

                // null check on the target. if target is not null, we call 'TakeDamage'
                target?.Interact();

            }
        }
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


    public void ApplyUpgradeNow(PickupData pickup, inventoryItem.ItemType itemType)
    {

        switch (itemType)
        {
            case inventoryItem.ItemType.AdrenalineShot:
                /*
                movementSpeed += pickup.speed;
                if (movementSpeed > 20)
                    movementSpeed = 20; // Cap speed to a maximum value
                */
                break;
            case inventoryItem.ItemType.Shield:
                HP += pickup.health;
                if (HP > initialHP)
                    HP = initialHP; // Cap health to initial value
                UpdatePlayerHealthBarUI();
                break;
            case inventoryItem.ItemType.EMP:
                // Implement EMP effect here
                break;
            case inventoryItem.ItemType.CloakingDevice:
                // Implement Cloaking effect here
                break;
            case inventoryItem.ItemType.StunGrenade:
                // Implement Stun Grenade effect here

                break;
            case inventoryItem.ItemType.Bomb:
            // Implement Bomb effect here

            case inventoryItem.ItemType.Armor:
                break;
            case inventoryItem.ItemType.Accessory:
                break;
            case inventoryItem.ItemType.Misc:
                break;


        }
    }

    public void FootStep()
    {
        int arrayPos = Random.Range(0, footSteps.Length - 1);

        AudioSource.PlayClipAtPoint(footSteps[arrayPos], transform.position);
       
    }

    public void spawnPlayer()
    {
        transform.position = GameManager.instance.playerSpawnPos.transform.position;

        HP = initialHP;
        UpdatePlayerHealthBarUI();
        stamina = initialStamina;
        UpdatePlayerStaminaBarUI();
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Start()
    {
        InitializeControllers();

        // setting the initial HP and stamina for bar processing //
        initialHP = HP;
        initialStamina = stamina;
        maxEXP = 500;

        //spawnPlayer();
        // Setting health bar to fill to the set amount at game start up
        UpdatePlayerHealthBarUI();
        UpdatePlayerEXPBarUI();

        InfoManager.instance.ShowMessage("ESCAPE!", "Use WASD to move, Shift to sprint, Space to jump, Ctrl to crouch, Left Click to shoot, R to reload, E to interact, Mouse Wheel to switch weapons.", Color.lightBlue, 10);
    }

    private void Update()
    {
        UpdateControllers();

        UpdateInteract();
        UpdateStamina();
        UpdatePlayerEXPBarUI();
        UpdatePlayerStaminaBarUI();
        UpdatePlayerHealthBarUI();

        HUDManager.instance.updatePlayerEXP(startingEXP, maxEXP);
        HUDManager.instance.updateHealthValue(HP);
        HUDManager.instance.updateStaminaValue((int)stamina);
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
