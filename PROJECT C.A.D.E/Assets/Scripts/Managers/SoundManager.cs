using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance; // Singleton instance for easy access

    public AudioSource soundSource; // Reference to the AudioSource component

    // public GameObject soundManagerPrefab;

    //Sound array for victory
    [SerializeField] AudioClip[] victorySounds;
    [SerializeField][Range(0f, 1f)] float victoryVolume = 0.5f;

    [SerializeField] AudioClip[] loseSounds;
    [SerializeField][Range(0f, 1f)] float loseVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float UI3D = 0;

    [SerializeField] AudioClip[] enemyShootSounds;
    [SerializeField] AudioClip[] enemyHitSounds;
    [SerializeField][Range(0f, 1f)] float enemyHitVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float enemyHit3D;
    [SerializeField][Range(0, 256)] int priorityEHit = 128;

    [SerializeField] AudioClip[] enemyDeathSounds;
    [SerializeField][Range(0f, 1f)] float enemyDeathVolume = 0.25f;
    [SerializeField][Range(0f, 0.80f)] float enemyDeath3D;
    [SerializeField][Range(0, 256)] int priorityEDeath = 128;

    [SerializeField] AudioClip[] bossEntranceSound;
    [SerializeField][Range(0f, 1f)] float bossEntranceVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float bossEntrance3D;
    [SerializeField][Range(0, 256)] int priorityBEntrance = 128;

    [SerializeField] AudioClip[] bossHitSounds;
    [SerializeField][Range(0f, 1f)] float bossHitVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float bossHit3D;
    [SerializeField][Range(0, 256)] int priorityBHit = 128;

    [SerializeField] AudioClip[] bossDeathSounds;
    [SerializeField][Range(0f, 1f)] float bossDeathVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float bossDeath3D;
    [SerializeField][Range(0, 256)] int priorityBDeath = 128;

    [SerializeField] AudioClip[] wallHitSounds;
    [SerializeField][Range(0f, 1f)] float wallHitVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float wallHit3D;
    [SerializeField][Range(0, 256)] int priorityWHit = 128;

    [SerializeField] AudioClip[] wallDestroySounds;
    [SerializeField][Range(0f, 1f)] float wallDestroyVolume = 0.5f;
    [SerializeField][Range(0f, 0.80f)] float wallDestroy3D;
    [SerializeField][Range(0, 256)] int priorityWDestroy = 128;


    // Reference to the game manager
    // public gameManager gameManager;


    public bool isPlaying;

    // Reference to the player controller script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        // Ensure that the sound manager persists across scenes
        //DontDestroyOnLoad(gameObject);
    }


    public void playVictorySound()
    {
        playClip(victorySounds, victoryVolume, UI3D, 128);
    }

    public void playLoseSound()
    {
        playClip(loseSounds, loseVolume, UI3D, 128);
    }

    public void playEnemyShootSound(Transform objectPosition)
    {

        playRandOneShot(objectPosition, enemyShootSounds, enemyHitVolume, enemyHit3D, priorityEHit);
    }

    public void playEnemyHitSound(Transform objectPosition)
    {
        playSFX(objectPosition, enemyHitSounds, enemyHitVolume, enemyHit3D, priorityEHit);
    }


    // Code for playing enemy death sound
    public void playEnemyDeathSound(Transform objectPosition)
    {
        playSFX(objectPosition, enemyDeathSounds, enemyDeathVolume, enemyDeath3D, priorityEDeath);
    }

    // Code for playing boss entrance sound
    public void playBossEntranceSound(Transform objectPosition)
    {
        playSFX(objectPosition, bossEntranceSound, bossEntranceVolume, bossEntrance3D, priorityBEntrance);
    }

    // Code for playing boss hit sound
    public void playBossHitSound(Transform objectPosition)
    {
        playSFX(objectPosition, bossHitSounds, bossHitVolume, bossHit3D, priorityBHit);
    }
    // Code for playing boss death sound
    public void playBossDeathSound(Transform objectPosition)
    {
        playSFX(objectPosition, bossDeathSounds, bossDeathVolume, bossDeath3D, priorityBDeath);
    }
    // Code for playing wall hit sound
    public void playWallHitSound(Transform objectPosition)
    {
        playSFX(objectPosition, wallHitSounds, wallHitVolume, wallHit3D, priorityWHit);
    }
    // Code for playing wall destroy sound
    public void playWallDestroySound(Transform objectPosition)
    {

        playSFX(objectPosition, wallDestroySounds, wallDestroyVolume, wallDestroy3D, priorityWDestroy);
    }


    // Play sound array method
    public void playClip(AudioClip[] clips, float volume, float sound3D, int priority)
    {
        AudioSource audioSource = Instantiate(soundSource, transform.position, Quaternion.identity);

        if (clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);

            AudioClip clip = clips[randomIndex];
            Debug.Log("Playing sound: " + clip.name);
            soundSource.spatialBlend = sound3D;  // Set to 3D sound
            audioSource.volume = volume;
            audioSource.spatialBlend = sound3D;
            audioSource.priority = priority;
            audioSource.Play();
            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }
        else
        {
            Debug.LogWarning("No audio clips assigned!");
        }
    }
    public void playSFX(Transform objectPosition, AudioClip[] clips, float volume, float sound3D, int priority)
    {
        AudioSource audioSource = Instantiate(soundSource, objectPosition.position, Quaternion.identity);

        if (clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);

            AudioClip clip = clips[randomIndex];
            audioSource.clip = clip;

            Debug.Log("Playing sound: " + clip.name);

            audioSource.volume = volume;
            audioSource.spatialBlend = sound3D;
            audioSource.priority = priority;
            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);

        }
        else
        {
            Debug.LogWarning("No audio clips assigned!");
        }

    }

    // Play one shot sound method with transform parameter
    public void playOneShot(Transform objectPosition, AudioClip clip, float volume, float sound3D, int priority)
    {
        AudioSource audioSource = Instantiate(soundSource, objectPosition.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = sound3D;
        audioSource.priority = priority;
        audioSource.PlayOneShot(clip);
    }

    public void playRandOneShot(Transform objectPosition, AudioClip[] clips, float volume, float sound3D, int priority)
    {
        AudioSource audioSource = Instantiate(soundSource, objectPosition.position, Quaternion.identity);
        if (clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioClip clip = clips[randomIndex];
            audioSource.clip = clip;
            Debug.Log("Playing sound: " + clip.name);
            audioSource.volume = volume;
            audioSource.spatialBlend = sound3D;
            audioSource.priority = priority;
            audioSource.PlayOneShot(clip);
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
        else
        {
            Debug.LogWarning("No audio clips assigned!");
        }
    }
}
