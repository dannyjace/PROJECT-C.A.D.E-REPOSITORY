using UnityEngine;

public class MinimapPickup : MonoBehaviour, IInteractable
{

    public MinimapModule moduleData; // Reference to the PickupData ScriptableObject
    public float rotateSpeed = 50f; // Speed at which the pickup rotates for visibility
    public float pulseSpeed = 2f; // Speed of the pulsing effect
    public float pulseMagnitude = 0.1f; // Magnitude of the pulsing effect
    private float pulseTimer = 0f; // Timer for pulsing effect


    private Vector3 originalPosition;
    private Vector3 initialScale; // Initial scale of the pickup for pulsing effect
    private Vector3 initialRotation;
    private Quaternion rotation;

    private void Start()
    {
        originalPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.eulerAngles;
        rotation = Quaternion.Euler(initialRotation);
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
        HUDManager.instance.UpdateInteractPrompt("");


        MinimapManager.instance.ActivateModule(moduleData);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            HUDManager.instance.interactPromptText.color = Color.white;
            HUDManager.instance.UpdateInteractPrompt("Press 'E' to pick up " + moduleData.name);

        }

    }

}
