using UnityEngine;

public class DropOff : MonoBehaviour
{
    [SerializeField] private Transform dropOffPoint;
    public Transform DropOffPoint => dropOffPoint;
    [SerializeField][Range(1, 25)] private int speed;
    [SerializeField] private GameObject parachute;



    private bool isDroppedOff = false;
    public bool IsDroppedOff => isDroppedOff;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDroppedOff)
        {
            moveTowardsDropOffPoint();
            checkForGround();
        }

    }

    private void checkForGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.transform == dropOffPoint)
                speed = Mathf.Max(1, speed / 2);


        }
    }
    private void moveTowardsDropOffPoint()
    {

        //transform.position = Vector3.MoveTowards(transform.position, dropOffPoint.position, Time.deltaTime * dropOffDuration);

        if (!isDroppedOff)
        {
            Vector3 direction = (dropOffPoint.position - transform.position).normalized;
            float step = speed * Time.deltaTime;
            transform.position += direction * step;
        }
        if (Vector3.Distance(transform.position, dropOffPoint.position) < 0.1f)
        {
            transform.position = dropOffPoint.position;
            isDroppedOff = true;
            Destroy(parachute);

        }
    }

}
