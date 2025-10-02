using UnityEngine;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Audio Controller Data", menuName = "Revolution Studios/Data/Player/Audio Controller Data")]
    public class PlayerAudioControllerData : ScriptableObject
    {
        [Header("Default Step Audio Clips")]
        [Space(10)]
        public AudioClip[] defaultStepClips;
        [Space(5)]
        [Range(0f, 1f)] public float stepVolume;
    }
}

