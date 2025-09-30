using UnityEngine;

public class PlayerPickup : MonoBehaviour, IInteractable
{
    public inventoryItem item;

    public PickupData pickup;
    public float rotateSpeed = 50f; // Speed at which the pickup rotates for visibility
    public float pulseSpeed = 2f; // Speed of the pulsing effect
    public float pulseMagnitude = 0.1f; // Magnitude of the pulsing effect
    private float pulseTimer = 0f; // Timer for pulsing effect


    private Vector3 originalPosition;
    private Vector3 initialScale; // Initial scale of the pickup for pulsing effect
    private Vector3 initialRotation;
    private Quaternion rotation;

    private inventoryItem.ItemType itemType;
    private void Start()
    {
        //gun = gameObject.GetComponent<inventoryItem>();
        originalPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.eulerAngles;
        rotation = Quaternion.Euler(initialRotation);
        itemType = item.itemType;
    }

    private void Update()
    {
        // Optional: Add any rotation or animation to the pickup object for visual effect
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // Rotate around the Y-axis
        // Pulsing effect with up and down movement
        pulseTimer += Time.deltaTime * pulseSpeed;
        float scaleFactor = 1 + Mathf.Sin(pulseTimer) * pulseMagnitude;
        transform.localScale = initialScale * scaleFactor;
        transform.position = originalPosition + new Vector3(0, Mathf.Sin(pulseTimer) * pulseMagnitude, 0); // Adjust the Y position for pulsing effect


    }

    public void Interact()
    {
        Debug.Log("Should be picking up");

        // Wait for enter key press to apply upgrade
        //if (Input.GetKeyDown("Enter"))
        //{
        //    GameManager.instance.playerScript.ApplyUpgradeNow(playerPickup);
        //}

        //else if (Input.GetKeyDown("E"))
        //{
        //    GameManager.instance.playerScript.AddItem(item);

        //    Destroy(gameObject);
        //}

        //else if (GameManager.instance.playerScript.HasItem(item))
        //    GameManager.instance.UpdateInteractPrompt("You already own the " + item.name);
        GameManager.instance.playerScript.ApplyUpgradeNow(pickup, itemType);
        Destroy(gameObject);
        HUDManager.instance.UpdateInteractPrompt("");
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            HUDManager.instance.UpdateInteractPrompt("Press 'E' to use " + item.itemName);
            HUDManager.instance.interactPromptText.color = Color.white;
        }

    }
}
