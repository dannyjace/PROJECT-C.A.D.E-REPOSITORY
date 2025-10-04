using UnityEngine;

public class FirearmGrip : MonoBehaviour
{
    [Header("Grip Settings")]
    [Space(10)]
    [SerializeField] private Transform leftHandIKTarget;


    public Transform LeftHandIKTarget { get { return leftHandIKTarget; }}
}
