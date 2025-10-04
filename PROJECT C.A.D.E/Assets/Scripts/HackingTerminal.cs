using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class HackingTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] interactions;
    [SerializeField] float openDelay;
    [SerializeField] float betweenDoorDelay;


    [Header("MINIGAME SETTINGS")]
    [SerializeField] GameObject hackMinigame;
    [SerializeField] TMP_Text prompt;
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
        StartCoroutine(IntractInSequence());
    }

    public void CompareInput()
    {
        if (textInput.text == "CADE")
        {
            Time.timeScale = 1;
            HackSuccess();
            hackMinigame.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            prompt.text = "Inncorrect, Type CADE to Hack";
        }
    }

    IEnumerator IntractInSequence()
    {
        for(int i = 0; i < interactions.Length; i++)
        {
            CinemachineCamera cam =  interactions[i].GetComponentInChildren<CinemachineCamera>();
            IInteractable interactable = interactions[i].GetComponent<IInteractable>();

            if (interactable != null && cam != null)
            {
                cam.Priority = i + 11;
                yield return new WaitForSeconds(openDelay);
                interactable.Interact();
            }
            yield return new WaitForSeconds(betweenDoorDelay);
            cam.Priority = 0;
        }

    }
}
