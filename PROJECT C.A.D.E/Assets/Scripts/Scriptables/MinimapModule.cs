using UnityEngine;

[CreateAssetMenu(fileName = "MinimapModule", menuName = "MinimapModule")]
public class MinimapModule : ScriptableObject
{

    [Header("Module Identity")]
    public GameObject modelPrefab; // Reference to the 3D model prefab
    public string moduleName;
    public string description;
    public Sprite icon;
    public Sprite reticalImage;




    [Header("Lore Feedback")]
    [TextArea]
    public string activationMessage;
    public AudioClip activationSound;
}
