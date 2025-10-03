using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Revolution Studios/Data/Projectiles/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Header("POOL SETTINGS")]
    [Space(5)]
    [SerializeField] private ObjectPool<Projectile> _projectilePool;
    [SerializeField] private float _activePoolCount;
    [SerializeField] private float _inactivePoolCount;
    [Space(20)]

    [Header("PROJECTILE SETTINGS")]
    [Space(5)]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;

    public ObjectPool<Projectile> ProjectilePool { get { return _projectilePool; } set { _projectilePool = value; } }
    public float ActivePoolCount { get { return _activePoolCount; } }
    public float InactivePoolCount { get { return _inactivePoolCount; } }
    public float ProjectileSpeed { get { return _projectileSpeed; } }
    public GameObject ProjectilePrefab { get { return _projectilePrefab; } }
}