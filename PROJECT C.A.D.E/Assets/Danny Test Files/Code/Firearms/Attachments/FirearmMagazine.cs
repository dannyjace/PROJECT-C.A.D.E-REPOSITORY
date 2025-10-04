using UnityEngine;

public class FirearmMagazine : MonoBehaviour
{
    [Header("Magazine Settings")]
    [Space(10)]
    [SerializeField] private int currentCapacity;
    [Space(5)]
    [SerializeField] private int maximumCapacity;
    [Space(10)]
    [SerializeField] private GameObject animationPrefab;


    public int CurrentCapacity { get { return currentCapacity; } }
    public int MaximumCapacity { get { return maximumCapacity; } }
    public GameObject AnimationPrefab { get { return animationPrefab; } }
}
