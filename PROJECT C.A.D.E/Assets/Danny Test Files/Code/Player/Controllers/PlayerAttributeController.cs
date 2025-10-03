using RevolutionStudios.Player.Utilities;
using RevolutionStudios.Player.Data;
using UnityEngine;

public class PlayerAttributeController
{
    private readonly PlayerController playerController;
    private readonly PlayerAttributeControllerData attributeControllerData;


    private int currentHealth;
    private int initialHealth;

    private int currentStamina;
    private int initialStamina;

    private int currentExperience;
    private int maximumExperience;


    public int CurrentHealth => currentHealth;
    public int InitialHealth => initialHealth;

    public int CurrentStamina => currentStamina;
    public int InitialStamina => initialStamina;

    public int CurrentExperience => currentExperience;
    public int MaximumExperience => maximumExperience;


    public PlayerAttributeController(PlayerController player, PlayerAttributeControllerSettings settings)
    {
        playerController = player;
        attributeControllerData = settings.data;
    }

    public void Initialize()
    {
        currentHealth = attributeControllerData.currentHealth;
        initialHealth = currentHealth;

        currentStamina = attributeControllerData.currentStamina;
        initialStamina = currentStamina;

        currentExperience = attributeControllerData.currentExperience;
        maximumExperience = attributeControllerData.maximumExperience;
    }

    public void Update()
    {
        UpdateStamina();
        UpdateInteraction();
    }


    private void UpdateStamina()
    {
        if (playerController.LocomotionState == PlayerLocomotionState.Sprinting && currentStamina > 0)
        {
            currentStamina -= (int)(attributeControllerData.staminaDegenerationRate * Time.deltaTime);
        }
        else if (playerController.LocomotionState == PlayerLocomotionState.Default && currentStamina < initialStamina)
        {
            currentStamina += (int)(attributeControllerData.staminaRegenerationRate * Time.deltaTime);
        }
    }
    private void UpdateInteraction()
    {
        /*

        if (inputController.InteractHeld)
        {
            if (Physics.Raycast(playerController.PlayerCamera.transform.position, playerController.PlayerCamera.transform.forward, out RaycastHit hit, attributeControllerData.interactionRange, ~attributeControllerData.interactionIgnoreLayer))
            {
                IInteractable target = hit.collider.GetComponent<IInteractable>();

                target?.Interact();
            }
        }

        */
    }
    public void AddExperience(int amount)
    {
        currentExperience += amount;

        if (currentExperience > maximumExperience)
        {
            currentExperience = currentExperience - maximumExperience;
            maximumExperience = maximumExperience * 2;
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        playerController.StartCoroutine(playerController.FlashDamageScreen());

        if (currentHealth <= 0)
        {
            // PLAYER HAS DIED //
        }
    }
}
