using UnityEngine;

[CreateAssetMenu(fileName = "gun", menuName = "Guns/GunData")]

public class WeaponData : ScriptableObject
{
    //public GameObject gunModel;
    public GameObject model;
    public enum GunType { Handgun, Shotgun, AutoRifle, Submachinegun, SniperRifle }

    public AudioClip pickUpSound;
    public AudioClip emptyClipSound;
    public AudioClip reloadSound;
    public AudioClip[] shootSounds;
    [Range(0, 1)] public float shootVolume = 0.5f; // Volume of the shooting sound

    public GunType gunType;
    public int shootDamage;
    public int shootDistance;
    public float shootRate;
    public int ammoCur;
    [Range(5, 50)] public int ammoMax; // Maximum ammo capacity
    public float reloadTime;

    public ParticleSystem impactEffect;
    public float impactForce;
    public ParticleSystem muzzleFlash;
    public Transform muzzleFlashPosition;

    [Header("Aim Settings")]
    [Space(10)]
    public Vector3 aimingPosePosition;
    public Vector3 aimingPoseRotation;
    [Space(20)]

    [Space(20)]
    [Header("Recoil Multipliers")]
    [Space(10)]
    [Range(0, 1f)] public float recoilPlayRate;
    [Space(5)]
    [Range(0, 100f)] public float recoilPositionSpeed;
    [Range(0, 100f)] public float recoilRotationSpeed;
    [Space(5)]
    [Range(-50f, 50f)] public float recoilXRotationMultiplier;
    [Range(-50f, 50f)] public float recoilYRotationMultiplier;
    [Range(-50f, 50f)] public float recoilZRotationMultiplier;
    [Space(5)]
    [Range(-50f, 50f)] public float recoilXPositionMultiplier;
    [Range(-50f, 50f)] public float recoilYPositionMultiplier;
    [Range(-50f, 50f)] public float recoilZPositionMultiplier;
    [Space(20)]

    [Header("Recoil Curves")]
    [Space(10)]
    public AnimationCurve recoilXRotationCurve;
    public AnimationCurve recoilYRotationCurve;
    public AnimationCurve recoilZRotationCurve;
    [Space(5)]
    public AnimationCurve recoilXPositionCurve;
    public AnimationCurve recoilYPositionCurve;
    public AnimationCurve recoilZPositionCurve;
}
