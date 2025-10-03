using UnityEngine;
using System;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Input Controller Data", menuName = "Revolution Studios/Data/Player/Input Controller Data")]
    public class PlayerInputControllerData : ScriptableObject
    {
        public event Action OnPlayerJumpPerformed;
        public event Action OnPlayerStanceChangePerformed;
        public event Action OnPlayerSprintPerformed;
        public event Action OnPlayerAimPerformed;
        public event Action OnPlayerAimCanceled;
        public event Action OnPlayerFirePerformed;
        public event Action OnPlayerFireCanceled;
        public event Action OnPlayerPausePerformed;

        public void PlayerJumpPerformed() => OnPlayerJumpPerformed?.Invoke();
        public void PlayerStanceChangePerformed() => OnPlayerStanceChangePerformed?.Invoke();
        public void PlayerSprintPerformed() => OnPlayerSprintPerformed?.Invoke();
        public void PlayerAimPerformed() => OnPlayerAimPerformed?.Invoke();
        public void PlayerAimCanceled() => OnPlayerAimCanceled?.Invoke();
        public void PlayerFirePerformed() => OnPlayerFirePerformed?.Invoke();
        public void PlayerFireCanceled() => OnPlayerFireCanceled?.Invoke();
        public void PlayerPausePerformed() => OnPlayerPausePerformed?.Invoke();
    }
}