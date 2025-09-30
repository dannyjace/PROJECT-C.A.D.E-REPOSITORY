using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlot;
    public inventoryItem[] inventoryItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public inventoryItem getItem()
    {
        return GameManager.instance.playerScript.inventory[GameManager.instance.playerScript.inventory.Count - 1];
    }

    public void useItem(string itemName)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].itemName == itemName)
            {
                inventoryItems[i].useItem();
            }
        }
    }
    public void addItem(inventoryItem item)
    {
        item = getItem();
        for (int i = 0;  i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].isFull == false)
            {
                inventorySlot[i].addItem(item);
                return;
            }
        }
    }
    public void deselectSlots()
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].highlightItem.SetActive(false);
            inventorySlot[i].itemActive = false;
        }
    }
}
