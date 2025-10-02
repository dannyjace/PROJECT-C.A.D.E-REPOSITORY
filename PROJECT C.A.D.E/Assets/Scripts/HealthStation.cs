using System.Collections;
using UnityEngine;

public class HealthStation : MonoBehaviour, IInteractable
{
    [SerializeField] Renderer objectRenderer;

    [SerializeField] Material activeMaterial;
    [SerializeField] int healAmount = 1;
    [SerializeField][Range(0, 2f)] private float coolDownTime = 1;
    private bool isHealing;
    private bool coolDownTimerActive = false;
    private float coolDownTimer;
    private Material origMaterial;
    //private Color originalColor;

    void Start()
    {
        origMaterial = objectRenderer.material;
        objectRenderer = GetComponent<Renderer>();
        isHealing = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUDManager.instance.UpdateInteractPrompt("Hold E to Heal...");
            HUDManager.instance.interactPromptText.color = Color.red;
        }
    }

    public void Interact()
    {
        /*
         
        if (GameManager.instance.playerScript.isFullyHealed)
        {
            isHealing = false;
            HUDManager.instance.interactPromptText.color = Color.green;
            HUDManager.instance.UpdateInteractPrompt("Fully Healed!");

            objectRenderer.material = origMaterial;
        }

        if (!GameManager.instance.playerScript.isFullyHealed)
        {
            if (!isHealing && !coolDownTimerActive)
            {
                isHealing = true;
                coolDownTimerActive = true;
                if (GameManager.instance.playerScript.isFullyHealed)
                {
                    StartCoroutine(HealOverTime());
                    StartCoroutine(CoolDownTimer());
                }

            }


        }

        */

    }



    private void OnTriggerExit(Collider other)
    {
        if (coolDownTimerActive)
        {
            HUDManager.instance.interactPromptText.color = Color.red;
            HUDManager.instance.UpdateInteractPrompt("Health Station is cooling down. Return in " + coolDownTimer);

        }
        HUDManager.instance.interactPromptText.color = Color.white;
        HUDManager.instance.UpdateInteractPrompt("");


        isHealing = false;

        objectRenderer.material = origMaterial;

    }

    private IEnumerator HealOverTime()
    {
        objectRenderer.material = activeMaterial;
        //GameManager.instance.playerScript.FillPlayerHPBar(healAmount);
        yield return new WaitForSeconds(1f);

    }

    private IEnumerator CoolDownTimer()
    {

        coolDownTimer = coolDownTime;
        coolDownTime -= Time.deltaTime;
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(coolDownTime);
        coolDownTime = 0f;
        coolDownTimerActive = false;
        isHealing = false;
        objectRenderer.material = origMaterial;
    }

}
