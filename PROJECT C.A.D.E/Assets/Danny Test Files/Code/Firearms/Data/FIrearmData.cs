using UnityEngine;

[CreateAssetMenu(fileName = "New Firearm Data", menuName = "Revolution Studios/Data/Firearms/New Firearm Data")]

public class FIrearmData : ScriptableObject
{
    [Header("Firearm Settings")]
    [Space(10)]
    [SerializeField, Range(0.01f, 1f)] public float fireRate;
    [Space(10)]
    [SerializeField] public bool ejectOnFire;
    [Space(20)]

    [Header("Pose Settings")]
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
