using UnityEngine;
using UnityEngine.UI;

public class ItemIterator : MonoBehaviour
{
    inventoryItem item;
    InventoryManager inventoryManager;
    InventorySlot slot;

    public int selectedItem = 0;
    public int selectedWeapon = 0;

    public Sprite itemSprite;
    public bool isFull;
    [SerializeField] public Image itemImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectItem();
        selectWeapon();
        //inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        //slot = GameObject.Find("InventorySlot").GetComponent<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        int previousItem = selectedItem;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (selectedItem == 0)
            {
                useItem();
            }
            selectedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (selectedItem == 1)
            {
                useItem();
            }
            selectedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (selectedItem == 2)
            {
                useItem();
            }
            selectedItem = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (selectedItem == 3)
            {
                useItem();
            }
            selectedItem = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (selectedItem == 4)
            {
                useItem();
            }
            selectedItem = 4;
        }

        if (selectedItem != previousItem)
            selectItem();
    }
    public void selectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selectedItem)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            i++;
        }
    }
    public void selectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void useItem()
    {
        inventoryManager.useItem(item.itemName);
        itemSprite = itemImage.sprite;
        isFull = false;

        slot.itemName = "";
        slot.itemSprite = slot.itemDImage.sprite;
        slot.itemDName.text = "";
        slot.itemDescriptionText.text = "";
        slot.isFull = false;
    }
}
