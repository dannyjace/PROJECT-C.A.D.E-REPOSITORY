using UnityEngine;

public class FirearmScope : FirearmAttachment
{
    [Space(20)]
    [Header("Scope Settings")]
    [Space(10)]
    [SerializeField] private Vector3 scopeAimOffset;


    public Vector3 ScopeAimOffset { get { return scopeAimOffset; } }
}
