using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] inventoryItem item;
    [TextArea(16, 10)][SerializeField] private string itemDescription;

    private InventoryManager inventoryManager;


   public float rotateSpeed = 50f; // Speed at which the pickup rotates for visibility
    public float pulseSpeed = 2f; // Speed of the pulsing effect
    public float pulseMagnitude = 0.1f; // Magnitude of the pulsing effect
    private float pulseTimer = 0f; // Timer for pulsing effect


    private Vector3 originalPosition;
    private Vector3 initialScale; // Initial scale of the pickup for pulsing effect
    private Vector3 initialRotation;
    private Quaternion rotation;

    void Start()
    {
        originalPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.eulerAngles;
        rotation = Quaternion.Euler(initialRotation);
        //inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    void Update()
    {
        // Optional: Add any rotation or animation to the pickup object for visual effect
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // Rotate around the Y-axis
        // Pulsing effect with up and down movement
        pulseTimer += Time.deltaTime * pulseSpeed;
        float scaleFactor = 1 + Mathf.Sin(pulseTimer) * pulseMagnitude;
        transform.localScale = initialScale * scaleFactor;
        transform.position = originalPosition + new Vector3(0, Mathf.Sin(pulseTimer) * pulseMagnitude, 0); // Adjust the Y position for pulsing effect

        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            HUDManager.instance.UpdateInteractPrompt("Press 'E' to pick up " + item.itemName);
            HUDManager.instance.interactPromptText.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HUDManager.instance.UpdateInteractPrompt("");
    }

    public void Interact()
    {
        Debug.Log("Should be picking up");
        GameManager.instance.playerScript.AddItem(item);
        //inventoryManager.addItem(item);
        
        Destroy(gameObject);
        HUDManager.instance.UpdateInteractPrompt("");  
         
    }
}
