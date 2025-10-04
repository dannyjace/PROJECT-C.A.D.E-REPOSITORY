using System.Collections;
using TMPro;
using UnityEngine;

public class HackingTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] interactions;

    [SerializeField] GameObject hackMinigame;
    [SerializeField] TMP_InputField textInput;



    public void Interact()
    {
        Time.timeScale = 0;
        hackMinigame.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
        
    }

    private void HackSuccess()
    {
        GameManager.instance.PlayerController.AnimationController.Hack();

        foreach (var interaction in interactions)
        {
            IInteractable interactable = interaction.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void CompareInput()
    {
        string input = textInput.text;

        if (input == "CADE")
        {
            Time.timeScale = 1;
            HackSuccess();
            hackMinigame.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
