using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    #region PROJECTILE FIELDS

    [Header("PROJECTILE SETTINGS")]
    [Space(10)]
    [SerializeField] private GameObject _projectileMesh;
    [SerializeField] private VisualEffect _projectileImpactVisualEffect;
    [Space(5)]
    [SerializeField] private LayerMask[] _projectileIgnoreLayers;

    private Rigidbody _projectileRigidbody;
    private CapsuleCollider _projectileCollider;
    private RaycastHit _hitPoint;
    private IObjectPool<Projectile> _projectilePrefabPool;
    private IObjectPool<Projectile> _impactPrefabPool;
    private float _projectileLifeTime = 5f;
    private float _projectileTimer;
    private bool _hit;

    #endregion

    #region PROJECTILE GETTERS

    public GameObject ProjectileMesh { get { return _projectileMesh; } }
    public VisualEffect ProjectileImpactVisualEffect { get { return _projectileImpactVisualEffect; } }
    public Rigidbody ProjectileRigidbody { get { return _projectileRigidbody; } }
    public CapsuleCollider ProjectileCollider { get { return _projectileCollider; } }
    public LayerMask[] ProjectileIgnoreLayers { get { return _projectileIgnoreLayers; } }

    #endregion

    #region PROJECTILE FUNCTIONS

    public void SetProjectilePool(IObjectPool<Projectile> projectilePool) => _projectilePrefabPool = projectilePool;
    private void UpdateProjectileLifeTime()
    {
        if (!_hit)
        {
            _projectileTimer += 1 * Time.deltaTime;

            if (_projectileTimer > _projectileLifeTime)
            {
                ReleaseProjectileToPool();
            }
        }
    }
    private void UpdateProjectileCollisionCheck()
    {
        if (!_hit)
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hitPoint, 1f, ~_projectileIgnoreLayers.Length))
            {
                StartCoroutine(HandleProjectileCollision());
                _hit = true;
            }
        }
    }
    private IEnumerator HandleProjectileCollision()
    {
        _projectileImpactVisualEffect.Play();
        _projectileRigidbody.linearVelocity = Vector3.zero;
        _projectileRigidbody.angularVelocity = Vector3.zero;
        _projectileCollider.enabled = false;
        _projectileMesh.SetActive(false);

        yield return new WaitForSecondsRealtime(2.5f);

        ReleaseProjectileToPool();
    }
    public void ResetProjectile()
    {
        _projectileRigidbody.linearVelocity = Vector3.zero;
        _projectileRigidbody.angularVelocity = Vector3.zero;
        _projectileTimer = 0;
        _projectileCollider.enabled = true;
        _projectileMesh.SetActive(true);
        _hit = false;
    }
    public void ReleaseProjectileToPool()
    {
        ResetProjectile();
        _projectilePrefabPool.Release(this);
    }

    #endregion

    #region MONOBEHAVIOUR

    private void OnEnable()
    {
        if (_projectileRigidbody == null) { _projectileRigidbody = transform.GetComponent<Rigidbody>(); }
        if (_projectileCollider == null) { _projectileCollider = transform.GetComponent<CapsuleCollider>(); }

        ResetProjectile();
    }
    private void OnDisable()
    {
        ResetProjectile();
    }
    private void Update()
    {
        UpdateProjectileLifeTime();
    }
    private void FixedUpdate()
    {
        UpdateProjectileCollisionCheck();
    }

    #endregion
}
