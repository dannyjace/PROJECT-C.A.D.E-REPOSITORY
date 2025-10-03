using System.Collections.Generic;
using UnityEngine;

public class FirearmAttachmentController : MonoBehaviour
{
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private bool updateAttachments;

    [Header("Attachments")]
    [Space(10)]
    [SerializeField] private List<FirearmAttachment> scopes = new List<FirearmAttachment>();
    [SerializeField] private int scopeIndex;
    [Space(10)]
    [SerializeField] private List<FirearmAttachment> muzzles = new List<FirearmAttachment>();
    [SerializeField] private int muzzleIndex;
    [Space(10)]
    [SerializeField] private List<FirearmAttachment> grips = new List<FirearmAttachment>();
    [SerializeField] private int gripIndex;
    [Space(10)]
    [SerializeField] private List<FirearmAttachment> magazines = new List<FirearmAttachment>();
    [SerializeField] private int magazineIndex;


    public FirearmScope CurrentScope { get { return scopes[scopeIndex].GetComponent<FirearmScope>(); } }
    public FirearmMuzzle CurrentMuzzle { get { return muzzles[muzzleIndex].GetComponent<FirearmMuzzle>(); } }
    public FirearmGrip CurrentGrip { get { return grips[gripIndex].GetComponent<FirearmGrip>(); } }
    public FirearmMagazine CurrentMagazine { get { return magazines[magazineIndex].GetComponent<FirearmMagazine>(); } }


    private void UpdateAttachment(List<FirearmAttachment> attachments, int index)
    {
        for (int i = 0; i < attachments.Count; i++)
        {
            attachments[i].gameObject.SetActive(false);

            attachments[index].gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        UpdateAttachment(scopes, scopeIndex);
        UpdateAttachment(muzzles, muzzleIndex);
        UpdateAttachment(grips, gripIndex); 
        UpdateAttachment(magazines, magazineIndex);
    }

    private void Update()
    {
        if (updateAttachments)
        {
            UpdateAttachment(scopes, scopeIndex);
            UpdateAttachment(muzzles, muzzleIndex);
            UpdateAttachment(grips, gripIndex);
            UpdateAttachment(magazines, magazineIndex);
        }
    }
}
