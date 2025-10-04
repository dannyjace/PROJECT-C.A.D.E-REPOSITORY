using UnityEngine;
using UnityEngine.VFX;

public class FirearmController : MonoBehaviour
{
    [SerializeField] private Transform leftHandIKTransform;
    [SerializeField] private VisualEffect muzzleFlashVFX;
    [SerializeField] private ParticleSystem muzzleSmokeVFX;
    [SerializeField] private VisualEffect ejectionPortSmokeVFX;
    [SerializeField] private ParticleSystem ejectionPortShellVFX;
    [SerializeField] private float fireRate;

    private bool aiming;
    private bool firing;

    private float fireTimer;

    private void Start()
    {
        SubscribeToInputEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToInputEvents();
    }

    private void Update()
    {
        UpdateFirearmFire();
    }


    private void UpdateFirearmFire()
    {
        fireTimer += Time.deltaTime;

        if (firing && fireTimer > fireRate)
        {
            PerformFire();
        }
    }
    private void PerformFire()
    {
        muzzleFlashVFX.Play();
        ejectionPortSmokeVFX.Play();
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
    }
    private void OnFirearmFireCanceled()
    {
        firing = false;

        muzzleSmokeVFX.Play();
    }
}
