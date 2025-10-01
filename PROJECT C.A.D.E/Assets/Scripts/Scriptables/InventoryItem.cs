using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "InventoryItem")]
public class inventoryItem : ScriptableObject
{
    public float staminaMod;
    public int shieldMod;
    public int armorMod;
    public int armorTimer;
    public float EMPTimer;
    public float EMPRange;
    public float stunTimer;
    public float stunRange;

    public void useItem()
    {
        if (itemType == ItemType.AdrenalineShot)
        {
            
        }
        else if (itemType == ItemType.Shield)
        {

        }
        else if (itemType == ItemType.EMP)
        {

        }
        else if (itemType == ItemType.CloakingDevice)
        {

        }
        else if (itemType == ItemType.StunGrenade)
        {

        }
        else if (itemType == ItemType.Armor)
        {

        }

        
    }
    public enum ItemType
    {
        AdrenalineShot,
        Shield,
        EMP,
        CloakingDevice,
        StunGrenade,
        Bomb,
        Weapon,
        Armor,
        Accessory,
        Misc
    };

    public ItemType itemType;
    public string itemName;
    [SerializeField]public Sprite icon;
    [TextArea(16, 10)] public string description;
}
