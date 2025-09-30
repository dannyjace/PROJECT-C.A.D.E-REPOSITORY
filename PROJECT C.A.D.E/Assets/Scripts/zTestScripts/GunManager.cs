using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    [Header("SETTINGS")]
    [Space(10)]
    [SerializeField] private Renderer weaponModel;
    [SerializeField] private WeaponData currentWeaponData;
    [Space(10)]
    [SerializeField] public LayerMask shootIgnoreLayer;
    [Space(10)]
    [SerializeField] AudioClip fireSound;
    public List<WeaponData> weaponList = new List<WeaponData>();

    private int weaponListIndex;
    private float shootTimer;
    private bool isReloading;

    private Vector3 targetRecoilRotation;
    private Vector3 targetRecoilPosition;

    private AdvancedPlayerController playerController;

    public WeaponData CurrentWeaponData => currentWeaponData;

    private void Start()
    {
        playerController = GetComponent<AdvancedPlayerController>();
        HUDManager.instance.DeactivateAmmoUI();
    }
    private void Update()
    {
        UpdateShoot();
        SelectWeapon();
        UpdateCurrentWeaponAmmoUI();

        if (Input.GetKeyDown(KeyCode.R))
        {
            AttemptReload();
        }
    }
    private void LateUpdate()
    {
        LateUpdateRecoil(playerController.WeaponRecoilPivot, playerController.MasterIK);
    }


    public void GetWeaponStats(WeaponData weaponData, inventoryItem weapon)
    {
        if (playerController.HasItem(weapon)) { return; }

        weaponList.Add(weaponData);
        weaponListIndex = weaponList.Count - 1;

        playerController.AddItem(weapon);
        HUDManager.instance.ActivateAmmoUI();

        ChangeWeapon();
    }
    private void ChangeWeapon()
    {
        currentWeaponData = weaponList[weaponListIndex];

        weaponModel.GetComponent<MeshFilter>().sharedMesh = weaponList[weaponListIndex].model.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weaponList[weaponListIndex].model.GetComponent<MeshRenderer>().sharedMaterial;

        SoundManager.instance.soundSource.PlayOneShot(weaponList[weaponListIndex].pickUpSound);
    }
    private void SelectWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && weaponListIndex < weaponList.Count - 1)
        {
            weaponListIndex++;
            ChangeWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && weaponListIndex > 0)
        {
            weaponListIndex--;
            ChangeWeapon();
        }
    }

    private bool CheckIfGunCanShoot()
    {
        return currentWeaponData.ammoCur > 0 && !isReloading;
    }
    private void UpdateShoot()
    {
        shootTimer += Time.deltaTime;

        if (weaponList.Count > 0)
        {
            if (Input.GetButton("Fire1") && CheckIfGunCanShoot() && shootTimer >= currentWeaponData.shootRate)
            {
                Shoot();
                AudioSource.PlayClipAtPoint(fireSound, transform.position);
            }
            else if (currentWeaponData.ammoCur <= 0 && !isReloading)
            {
                AttemptReload();
            }
        }
    }
    private void Shoot()
    {
        // resetting the shoot timer //
        shootTimer = 0;
        currentWeaponData.ammoCur--;
        PerformShoot();
        SetTargetRecoilMultipliers();
    }
    private void PerformShoot()
    {
        // performing shoot raycast //
        if (Physics.Raycast(playerController.PlayerCamera.transform.position, playerController.PlayerCamera.transform.forward, out RaycastHit hit, currentWeaponData.shootDistance, ~shootIgnoreLayer))
        {
            // logging the collider the raycast hit //
            Debug.Log(hit.collider.name);

            // if the collider has the IDamage interface, we store it in 'target'
            IDamage target = hit.collider.GetComponent<IDamage>();

            // null check on the target. if target is not null, we call 'TakeDamage'
            target?.TakeDamage(currentWeaponData.shootDamage);

            Instantiate(weaponList[weaponListIndex].impactEffect, hit.point, Quaternion.identity);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * currentWeaponData.impactForce);
            }
        }
    }

    private IEnumerator ReloadSequence()
    {
        isReloading = true;

        if (currentWeaponData.reloadSound != null)
        {
            AudioSource.PlayClipAtPoint(currentWeaponData.reloadSound, transform.position);
        }

        yield return new WaitForSeconds(currentWeaponData.reloadTime);
        currentWeaponData.ammoCur = currentWeaponData.ammoMax;
        isReloading = false;
    }
    public void AttemptReload()
    {
        if (weaponList.Count > 0)
        {
            if (isReloading || currentWeaponData.ammoCur >= currentWeaponData.ammoMax)
                return;

            StartCoroutine(ReloadSequence());
        }
    }

    private void UpdateCurrentWeaponAmmoUI()
    {
        if (weaponList.Count > 0)
        {
            HUDManager.instance.updatePlayerAmmo(currentWeaponData.ammoCur, currentWeaponData.ammoMax);
        }
    }

    private void LateUpdateRecoil(Transform recoilPivot, Transform masterIK)
    {
        if (weaponList.Count > 0)
        {
            float positionX = recoilPivot.localPosition.x + currentWeaponData.recoilXPositionCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilPosition.x;
            float positionY = recoilPivot.localPosition.y + currentWeaponData.recoilYPositionCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilPosition.y;
            float positionZ = recoilPivot.localPosition.z + currentWeaponData.recoilZPositionCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilPosition.z;

            float rotationX = recoilPivot.localRotation.x + currentWeaponData.recoilXRotationCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilRotation.x;
            float rotationY = recoilPivot.localRotation.y + currentWeaponData.recoilYRotationCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilRotation.y;
            float rotationZ = recoilPivot.localRotation.z + currentWeaponData.recoilZRotationCurve.Evaluate(shootTimer * currentWeaponData.recoilPlayRate) * targetRecoilRotation.z;

            Vector3 recoilPosition = new(positionX, positionY, positionZ);
            Vector3 recoilRotation = new(rotationX, rotationY, rotationZ);

            recoilPivot.localPosition = Vector3.Lerp(masterIK.localPosition, recoilPosition, Time.deltaTime * currentWeaponData.recoilPositionSpeed);
            recoilPivot.localRotation = Quaternion.Slerp(masterIK.localRotation, Quaternion.Euler(recoilRotation), Time.deltaTime * currentWeaponData.recoilRotationSpeed);
        }
    }
    private void SetTargetRecoilMultipliers()
    {
        targetRecoilRotation = new(currentWeaponData.recoilXRotationMultiplier, Random.Range(-currentWeaponData.recoilYRotationMultiplier, currentWeaponData.recoilYRotationMultiplier), Random.Range(-currentWeaponData.recoilZRotationMultiplier, currentWeaponData.recoilZRotationMultiplier));
        targetRecoilPosition = new(Random.Range(-currentWeaponData.recoilXPositionMultiplier, currentWeaponData.recoilXPositionMultiplier), currentWeaponData.recoilYPositionMultiplier, currentWeaponData.recoilZPositionMultiplier);
    }
}
