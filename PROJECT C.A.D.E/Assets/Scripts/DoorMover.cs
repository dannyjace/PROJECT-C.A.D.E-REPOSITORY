using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DoorMover : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0, 5)] float moveDist;
    [SerializeField, Range(0, 5)] float moveSpeed;
    [SerializeField] Vector3 moveDir;

    public CinemachineCamera doorCam;
    [SerializeField] float delay;

    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    private Vector3 leftClosedPosition;
    private Vector3 leftOpenPosition;

    private Vector3 rightClosedPosition;
    private Vector3 rightOpenPosition;

    private bool isOpening = false;

    void Start()
    {
        leftClosedPosition = leftDoor.transform.localPosition;
        leftOpenPosition = leftClosedPosition + moveDir * moveDist;

        rightClosedPosition = rightDoor.transform.localPosition;
        rightOpenPosition = rightClosedPosition + -moveDir * moveDist;
    }

    void Update()
    {
        if (isOpening)
        {
            leftDoor.transform.localPosition = Vector3.MoveTowards(leftDoor.transform.localPosition, leftOpenPosition, moveSpeed * Time.deltaTime);
            rightDoor.transform.localPosition = Vector3.MoveTowards(rightDoor.transform.localPosition, rightOpenPosition, moveSpeed * Time.deltaTime);
        }

    }

    public void Interact()
    {
        isOpening = true;
    }
}
