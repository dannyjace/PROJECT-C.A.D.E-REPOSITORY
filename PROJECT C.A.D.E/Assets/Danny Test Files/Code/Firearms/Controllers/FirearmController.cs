using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Pool;

public class FirearmController : MonoBehaviour
{
    [Header("Firearm Data")]
    [Space(10)]
    [SerializeField] private FirearmData data;
    [Space(20)]

    [Header("References")]
    [Space(10)]
    [SerializeField] private Transform leftHandIKTransform;
    [Space(10)]
    [SerializeField] private VisualEffect muzzleFlashVFX;
    [SerializeField] private ParticleSystem muzzleSmokeVFX;
    [SerializeField] private VisualEffect ejectionPortSmokeVFX;
    [SerializeField] private ParticleSystem ejectionPortShellVFX;


    private FirearmAttachmentController attachmentController;

    private bool aiming;
    private bool firing;

    private float fireTimer;

    private Vector3 targetRecoilPosition;
    private Vector3 targetRecoilRotation;

    private Transform recoilPivot;
    private Transform masterIKTransform;

    private void Start()
    {
        SubscribeToInputEvents();
        InitializeFirearm();
    }

    private void OnDisable()
    {
        UnsubscribeToInputEvents();
    }

    private void Update()
    {
        UpdateFirearm();
    }

    private void LateUpdate()
    {
        LateUpdateRecoil(recoilPivot, masterIKTransform);

        // TODO: add left hand ik update here <<<<
    }


    private void InitializeFirearm()
    {
        attachmentController = GetComponent<FirearmAttachmentController>();

        recoilPivot = GameManager.instance.PlayerController.WeaponRecoilPivot;
        masterIKTransform = GameManager.instance.PlayerController.MasterIK;

        InitializeProjectilePool();
    }
    private void UpdateFirearm()
    {
        UpdateFirearmFire();
    }
    private void UpdateFirearmFire()
    {
        fireTimer += Time.deltaTime;

        if (firing && (fireTimer > data.fireRate))
        {
            PerformFire();
            SetTargetRecoilMultipliers();
        }
    }
    private void PerformFire()
    {
        InstantiateProjectile();


        if (data.ejectOnFire)
        {
            //ejectionPortShellVFX.Emit(1);
        }

        muzzleFlashVFX.Play();
        ejectionPortSmokeVFX.Play();

        fireTimer = 0;
    }
    private void LateUpdateRecoil(Transform recoilPivot, Transform masterIK)
    {
        float positionX = recoilPivot.localPosition.x + data.recoilXPositionCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilPosition.x;
        float positionY = recoilPivot.localPosition.y + data.recoilYPositionCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilPosition.y;
        float positionZ = recoilPivot.localPosition.z + data.recoilZPositionCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilPosition.z;

        float rotationX = recoilPivot.localRotation.x + data.recoilXRotationCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilRotation.x;
        float rotationY = recoilPivot.localRotation.y + data.recoilYRotationCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilRotation.y;
        float rotationZ = recoilPivot.localRotation.z + data.recoilZRotationCurve.Evaluate(fireTimer * data.recoilPlayRate) * targetRecoilRotation.z;

        Vector3 recoilPosition = new(positionX, positionY, positionZ);
        Vector3 recoilRotation = new(rotationX, rotationY, rotationZ);

        recoilPivot.localPosition = Vector3.Lerp(masterIK.localPosition, recoilPosition, Time.deltaTime * data.recoilPositionSpeed);
        recoilPivot.localRotation = Quaternion.Slerp(masterIK.localRotation, Quaternion.Euler(recoilRotation), Time.deltaTime * data.recoilRotationSpeed);
    }
    private void SetTargetRecoilMultipliers()
    {
        targetRecoilRotation = new(data.recoilXRotationMultiplier, Random.Range(-data.recoilYRotationMultiplier, data.recoilYRotationMultiplier), Random.Range(-data.recoilZRotationMultiplier, data.recoilZRotationMultiplier));
        targetRecoilPosition = new(Random.Range(-data.recoilXPositionMultiplier, data.recoilXPositionMultiplier), data.recoilYPositionMultiplier, data.recoilZPositionMultiplier);
    }
    private Projectile CreateProjectile()
    {
        var projectile = Instantiate(data.projectilePrefab, Vector3.zero, Quaternion.identity, attachmentController.CurrentMuzzle.transform);
        projectile.GetComponent<Projectile>().SetProjectilePool(data.projectilePool);

        return projectile.GetComponent<Projectile>();
    }
    private void OnTakeProjectileFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(attachmentController.CurrentMuzzle.transform);
        projectile.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    private void OnReturnProjectileToPool(Projectile projectile)
    {
        projectile.transform.SetParent(attachmentController.CurrentMuzzle.transform);
        projectile.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        projectile.gameObject.SetActive(false);
    }
    private void InitializeProjectilePool()
    {
        data.projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool, defaultCapacity: 30, maxSize: 60);

        for (int i = 0; i < data.activePoolCount; i++)
        {
            CreateProjectile().ReleaseProjectileToPool();
        }
    }
    private void InstantiateProjectile()
    {
        var projectile = data.projectilePool.Get();
        projectile.transform.SetParent(attachmentController.CurrentMuzzle.transform);
        projectile.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        projectile.GetComponent<Rigidbody>().AddForce(GameManager.instance.PlayerController.MasterIK.forward * Time.deltaTime * data.projectileSpeed, ForceMode.Impulse);
        projectile.transform.SetParent(null);
    }


    private void SubscribeToInputEvents()
    {
        GameManager.instance.InputManager.OnPlayerAimPerformed += OnFirearmAimPerformed;
        GameManager.instance.InputManager.OnPlayerAimCanceled += OnFirearmAimCanceled;
        GameManager.instance.InputManager.OnPlayerFirePerformed += OnFirearmFirePerformed;
        GameManager.instance.InputManager.OnPlayerFireCanceled += OnFirearmFireCanceled;
    }
    private void UnsubscribeToInputEvents()
    {
        GameManager.instance.InputManager.OnPlayerAimPerformed -= OnFirearmAimPerformed;
        GameManager.instance.InputManager.OnPlayerAimCanceled -= OnFirearmAimCanceled;
        GameManager.instance.InputManager.OnPlayerFirePerformed -= OnFirearmFirePerformed;
        GameManager.instance.InputManager.OnPlayerFireCanceled -= OnFirearmFireCanceled;
    }


    private void OnFirearmAimPerformed()
    {
        aiming = true;
    }
    private void OnFirearmAimCanceled()
    {
        aiming = false;
    }

    private void OnFirearmFirePerformed()
    {
        firing = true;

        muzzleSmokeVFX.Stop();
    }
    private void OnFirearmFireCanceled()
    {
        targetRecoilRotation = new(0, 0, 0);
        targetRecoilPosition = new(0, 0, 0);

        firing = false;

        muzzleSmokeVFX.Play();
    }
}
