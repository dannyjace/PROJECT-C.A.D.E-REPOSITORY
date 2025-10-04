using UnityEngine;

public class DoorMover : MonoBehaviour, IInteractable
{
    [SerializeField] float moveDist;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 moveDir;

    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    private Vector3 leftClosedPosition;
    private Vector3 leftOpenPosition;

    private Vector3 rightClosedPosition;
    private Vector3 rightOpenPosition;

    private bool isOpening = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftClosedPosition = leftDoor.transform.localPosition;
        leftOpenPosition = leftClosedPosition + moveDir * moveDist;

        rightClosedPosition = rightDoor.transform.localPosition;
        rightOpenPosition = rightClosedPosition + -moveDir * moveDist;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {

            leftDoor.transform.localPosition = Vector3.MoveTowards(leftDoor.transform.localPosition, leftOpenPosition, moveSpeed * Time.deltaTime);
            rightDoor.transform.localPosition = Vector3.MoveTowards(rightDoor.transform.localPosition, rightOpenPosition, moveSpeed * Time.deltaTime);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void Open()
    {
        isOpening = true;

    }

    public void Close()
    {
        isOpening = false;
    }

    public void Interact()
    {

        Open();

    }
}
