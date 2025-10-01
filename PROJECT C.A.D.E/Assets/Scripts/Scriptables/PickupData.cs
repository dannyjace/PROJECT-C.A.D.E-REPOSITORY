using UnityEngine;

[CreateAssetMenu]
public class PickupData : ScriptableObject
{
    // Enum of player upgrades
    //public enum UpgradeType
    //{
    //    None,
    //    Speed,
    //    Health,
    //    Damage,
    //    Ammo,
    //    FireRate,
    //    ExtendedMag
    //}

    //[SerializeField] gunStats gun;
    //public inventoryItem.ItemType item;
    //public UpgradeType type; // Type of upgrade
    public GameObject model;
    public bool isEquippable; // Indicates if the pickup can be equipped (e.g., a gun)
                              // Upgrade type array


    [Range(1, 10)] public int level = 1; // Level of the upgrade
                                         //[Range(1, 100)] public int cost = 10; // Cost of the upgrade
                                         //[Range(1, 5)] public int expValue;

    public AudioClip pickupSound; // Sound played when the upgrade is picked up
    public AudioClip useSound; // Sound played when the upgrade is used
    public AudioClip dropSound; // Sound played when the upgrade is dropped
    public int speed; // Player speed
    public int sprintMod;
    public int health; // Player health
    public int damage;

    // Start is called before the first frame update
    [Range(10, 20)] public int shootDamage;
    [Range(10, 1000)] public int shootDist;
    [Range(0.1f, 3)] public float shootRate; // Time between shots in seconds
    [Range(5, 50)] public int ammoAdd;
    [Range(5, 50)] public int ammoMax; // Maximum ammo capacity

    // Extended Mag Options

    // public int extendedMagAmmoCur = 10; // Current ammo with extended mag
    // public int extendedMagAmmoMax = 50; // Maximum ammo with extended mag
    public ParticleSystem hitEffect;
    public AudioClip[] upgradeClips;
}
