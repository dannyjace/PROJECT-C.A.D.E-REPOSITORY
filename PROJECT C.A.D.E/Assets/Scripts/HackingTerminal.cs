using UnityEngine;

public class HackingTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject[] interactions;

    public void Interact()
    {
        foreach (var interaction in interactions)
        {
            IInteractable interactable = interaction.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
