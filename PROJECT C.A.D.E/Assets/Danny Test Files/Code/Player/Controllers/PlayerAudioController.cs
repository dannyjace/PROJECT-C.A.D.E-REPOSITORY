using RevolutionStudios.Player.Data;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

public class PlayerAudioController
{
    private readonly PlayerController playerController;
    private readonly PlayerInputController inputController;
    private readonly PlayerAudioControllerData audioControllerData;
    private readonly AudioSource footStepAudioSource;

    public PlayerAudioController(PlayerController player, PlayerAudioControllerSettings settings)
    {
        playerController = player;
        inputController = player.InputController;
        audioControllerData = settings.data;
        footStepAudioSource = settings.footStepAudioSource;
    }

    public void PlayFootStepAudio()
    {
        if (inputController.MoveInput.normalized.magnitude > Mathf.Epsilon && playerController.GroundedState == PlayerGroundedState.Grounded)
        {
            var clip = audioControllerData.defaultStepClips[Random.Range(0, audioControllerData.defaultStepClips.Length)];
            var volume = audioControllerData.stepVolume;
            var pitch = Random.Range(0.9f, 1.1f);

            footStepAudioSource.volume = volume;
            footStepAudioSource.pitch = pitch;
            footStepAudioSource.PlayOneShot(clip);
        }
    }
}
