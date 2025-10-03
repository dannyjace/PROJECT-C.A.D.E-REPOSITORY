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

    private PlayerController playerController;

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
        bool moving = playerController.InputController.MoveInput.magnitude > 0 || playerController.InputController.LookInput.magnitude > 0 || playerController.LocomotionState == PlayerLocomotionState.Sprinting || playerController.GroundedState == PlayerGroundedState.Airborne;

        if (moving)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        playerController = GameManager.instance.playerScript;
    }
    private void Update()
    {
        UpdateReticleSize();
    }
}
