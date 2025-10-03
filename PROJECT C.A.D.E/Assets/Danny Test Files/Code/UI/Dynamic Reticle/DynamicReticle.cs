using RevolutionStudios.Player.Utilities;
using UnityEngine;

public class DynamicReticle : MonoBehaviour
{
    [SerializeField] RectTransform reticle;
    [Space(10)]
    [SerializeField, Range(0, 100)] private float minimumSize;
    [SerializeField, Range(0, 250)] private float maximumSize;
    [Space(5)]
    [SerializeField, Range(0, 10)] private float sizeSpeed;

    private float targetSize;

    private void UpdateReticleSize()
    {
        if (ApplyDynamics())
        {
            targetSize = maximumSize;
        }
        else
        {
            targetSize = minimumSize;
        }

        reticle.sizeDelta = Vector2.Lerp(reticle.sizeDelta, new(targetSize, targetSize), Time.deltaTime * sizeSpeed);
    }
    private bool ApplyDynamics()
    {
        bool moving = GameManager.instance.InputManager.MoveInput.magnitude > 0 || GameManager.instance.InputManager.LookInput.magnitude > 0 || GameManager.instance.PlayerController.LocomotionState == PlayerLocomotionState.Sprinting || GameManager.instance.PlayerController.GroundedState == PlayerGroundedState.Airborne;

        if (moving)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        UpdateReticleSize();
    }
}
