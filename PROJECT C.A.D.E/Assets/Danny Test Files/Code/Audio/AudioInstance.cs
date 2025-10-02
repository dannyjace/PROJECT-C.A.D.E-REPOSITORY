using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class AudioInstance : MonoBehaviour
{
    #region FIELDS

    private AudioSource _audioSource;

    private IObjectPool<AudioInstance> _audioInstancePool;
    private float _instanceTimer;
    private float _instanceLifetime = 5f;

    #endregion

    #region GETTERS

    public float InstanceLifetime { get => _instanceLifetime; set => _instanceLifetime = value; }
    public AudioSource AudioSource { get => _audioSource; private set => _audioSource = value; }

    #endregion

    #region FUNCTIONS

    public void SetAudioInstancePool(IObjectPool<AudioInstance> AudioInstancePool) => _audioInstancePool = AudioInstancePool;
    public void ReleaseAudioInstanceToPool() => _audioInstancePool.Release(this);
    private void UpdateInstanceLifeTime()
    {
        _instanceTimer += Time.deltaTime;

        if (_instanceTimer >= _instanceLifetime)
        {
            ReleaseAudioInstanceToPool();
        }
    }

    #endregion

    #region MONOBEHAVIOURS

    private void OnEnable()
    {
        _instanceTimer = 0;

        if (_audioSource == null) { _audioSource = GetComponent<AudioSource>(); }
    }

    private void Update()
    {
        UpdateInstanceLifeTime();
    }

    #endregion
}
