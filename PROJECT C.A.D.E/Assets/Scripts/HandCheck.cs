using UnityEngine;

public class HandCheck : MonoBehaviour
{


    [SerializeField] private LayerMask handLayer;
    private Camera renderCamera;
    private GunManager gunManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderCamera = GetComponent<Camera>();
        gunManager = GetComponentInParent<GunManager>();

        if (gunManager.CurrentWeaponData == null)
        {
            // hide the hand layer mask in the render camera
            renderCamera.cullingMask &= ~handLayer;
        }




    }

    // Update is called once per frame
    void Update()
    {
        if (gunManager.CurrentWeaponData != null)
        {
            // show the hand layer mask in the render camera
            renderCamera.cullingMask |= handLayer;
        }
        else
        {
            // hide the hand layer mask in the render camera
            renderCamera.cullingMask &= ~handLayer;
        }


    }
}
