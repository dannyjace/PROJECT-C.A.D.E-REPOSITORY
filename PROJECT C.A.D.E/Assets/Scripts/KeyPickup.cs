using System.Collections;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    [SerializeField] inventoryItem key;

    [SerializeField] int rotationSpeed;
    [SerializeField] float scaleSpeed;
    [SerializeField] float scaleAmount;

    private Vector3 origScale;

    void Start()
    {
        origScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUDManager.instance.UpdateInteractPrompt("Press 'E' to pick up " + key.itemName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        HUDManager.instance.UpdateInteractPrompt("");
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float scaler = Mathf.PingPong(Time.time * scaleSpeed, 1f);
        float scale = 1 + scaler * scaleAmount;
        transform.localScale = origScale * scale;
    }


    public void Interact()
    {
        Debug.Log("Key Found");
        GameManager.instance.playerScript.AddItem(key);
        HUDManager.instance.UpdateKeyCountText();
        HUDManager.instance.UpdateInteractPrompt("");
        Destroy(gameObject);
    }


}
