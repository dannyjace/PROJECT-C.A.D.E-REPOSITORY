using System.Threading;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] float lifespan;

    private float objTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objTimer += Time.deltaTime;
        if (objTimer >= lifespan) { Destroy(gameObject); }
    }
}
