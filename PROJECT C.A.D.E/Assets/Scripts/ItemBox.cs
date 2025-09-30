using System.Collections;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    [Header("BOX SETTINGS")]
    [SerializeField] GameObject pivotPoint;
    [SerializeField] Vector3 rotDir;
    [SerializeField, Range(0, 100)] int rotSpeed;

    [Header("ITEM SETTINGS")]
    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] Vector3 itemOffset;
    [SerializeField, Range(.1f, 2.0f)] float dropDelay;
    
    private Quaternion targetRot;
    private Collider interactCollider;
    private bool isRotating = false;
    private bool used;

    void Start()
    {
        interactCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isRotating)
        {
            pivotPoint.transform.localRotation = Quaternion.RotateTowards(pivotPoint.transform.localRotation, targetRot, rotSpeed * Time.deltaTime);

            if (pivotPoint.transform.localRotation == targetRot)
            {
                isRotating = false;
            }
        }

        
    }

    public void Interact()
    {
        targetRot = Quaternion.Euler(rotDir);
        isRotating = true;
        interactCollider.enabled = false;
        StartCoroutine(SpawnItem());

    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(dropDelay);
        if(itemsToDrop != null)
        {
            int arrayPos = Random.Range(0, itemsToDrop.Length - 1);
            Instantiate(itemsToDrop[arrayPos], transform.position + itemOffset, Quaternion.identity);
        }
    }


   
}
