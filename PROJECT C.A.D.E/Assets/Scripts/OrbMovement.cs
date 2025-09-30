using UnityEngine;

public class OrbMovement : MonoBehaviour
{
    [SerializeField] Transform orbTransform;

    [SerializeField] float bobbingSpeed;
    [SerializeField] float bobbingAmount;
    [SerializeField] float offsetAmount;


    private void Update()
    {
        UpdateOrbMovement();
    }

    private void UpdateOrbMovement()
    {
        float movement = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        Vector3 position = new Vector3(0, 0, movement + offsetAmount);

        orbTransform.localPosition = position;
    }
}
