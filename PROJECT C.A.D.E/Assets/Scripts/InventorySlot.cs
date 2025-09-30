using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;
    public GameObject highlightItem;
    public bool itemActive;
    private InventoryManager inventoryManager;
    private ItemIterator itemBar;
    public Image selectedItem;
    public TMP_Text itemDName;
    public string itemDescription;
    public TMP_Text itemDescriptionText;

    [SerializeField] public Image itemDImage;
    [SerializeField] public Image itemImage;

    void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        //itemBar = GameObject.Find("ItemIterator").GetComponent<ItemIterator>();

    }
    public void addItem(inventoryItem item)
    {
        itemName = item.name;
        itemSprite = item.icon;
        itemDescription = item.description;
        isFull = true;
        itemDImage.sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.deselectSlots();
            highlightItem.SetActive(true);
            itemActive = true;
            itemDName.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDImage.sprite = itemSprite;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //add item to item bar

        }
    }
}
