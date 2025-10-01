using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class perkNode : MonoBehaviour
{
    public GameObject textBox;
    public GameObject nextAcquireButton;

    public Sprite availableNode;
    public Sprite unavailableNode;
    public Sprite purchasedNode;

    public Image currImage;
    public Image nextImage;



    private bool descripActive;


    public void showDescription()
    {
        descripActive = !descripActive;

        textBox.SetActive(descripActive);
    }

    public void purchaseSkill()
    {
        currImage.sprite = purchasedNode;
        currImage.rectTransform.sizeDelta = new Vector2(200, 200);

        if (nextImage != null && nextImage.sprite != purchasedNode)
        {
            nextImage.sprite = availableNode;
        }

        if (nextAcquireButton != null)
        {
            nextAcquireButton.SetActive(true);
        }
    }

}
