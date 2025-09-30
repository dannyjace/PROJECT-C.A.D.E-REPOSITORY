

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MinimapManager : MonoBehaviour
{

    public static MinimapManager instance; // Singleton instance for easy access

    [Header("Minimap Components")]
    [SerializeField] private GameObject minimapCanvas;

    public Camera minimapCam;
    [SerializeField] private Image reticalImage;


    [Header("Minimap Layer Activation")]
    [SerializeField] string[] mapLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        minimapCanvas.SetActive(false);
    }


    public void ActivateModule(MinimapModule moduleData)
    {

        EnableModule(moduleData);





    }


    // Switchcase for module types to enable specific layers or overlays based on module type
    private void EnableModule(MinimapModule moduleData)
    {


        reticalImage.enabled = true;
        foreach (string layerName in mapLayer)
        {
            int layer = LayerMask.NameToLayer(layerName);
            if (layer != -1)
            {
                minimapCam.cullingMask |= (1 << layer);
            }
            else
            {
                Debug.LogWarning("Layer not found: " + layerName);
            }
        }
        minimapCanvas.SetActive(true);

    }



}

