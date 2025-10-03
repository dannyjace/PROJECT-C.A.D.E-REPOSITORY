using UnityEngine;
using UnityEngine.VFX;

public class FirearmController : MonoBehaviour
{
    [Header("Firearm Data")]
    [Space(10)]
    [SerializeField] private FIrearmData data;
    [Space(20)]

    [Header("References")]
    [Space(10)]
    [SerializeField] private Transform leftHandIKTransform;
    [Space(10)]
    [SerializeField] private VisualEffect muzzleFlashVFX;
    [SerializeField] private ParticleSystem muzzleSmokeVFX;
    [SerializeField] private VisualEffect ejectionPortSmokeVFX;
    [SerializeField] private ParticleSystem ejectionPortShellVFX;

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
        recoilPivot = GameManager.instance.PlayerController.WeaponRecoilPivot;
        masterIKTransform = GameManager.instance.PlayerController.MasterIK;
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
