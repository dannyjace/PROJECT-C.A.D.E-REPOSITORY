using UnityEngine;
using UnityEngine.VFX;

public class FirearmMuzzle : FirearmAttachment
{
    [Space(20)]
    [Header("Muzzle Settings")]
    [Space(10)]
    [SerializeField] private Transform muzzleSocket;
    [Space(20)]

    [Header("VFX Settings")]
    [Space(10)]
    [SerializeField] private VisualEffect muzzleFlashVFX;
    [SerializeField] private ParticleSystem muzzleSmokeVFX;
    [Space(20)]

    [Header("Audio Settings")]
    [Space(10)]
    [SerializeField] private AudioSource muzzleAudioSource;
    [Space(5)]
    [SerializeField] private AudioClip[] muzzleFireClips;


    public Transform MuzzleSocket { get { return muzzleSocket; } }
    public VisualEffect MuzzleFlashVFX { get { return muzzleFlashVFX; } }
    public ParticleSystem MuzzleSmokeVFX { get { return muzzleSmokeVFX; } }
    public AudioSource MuzzleAudioSource { get { return muzzleAudioSource; } }
    public AudioClip FireClip { get { return muzzleFireClips[Random.Range(0, muzzleFireClips.Length)]; } }
}
