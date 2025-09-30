using System.Collections;
using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem[] sprinklers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartWater());
    }

   

    IEnumerator StartWater()
    {
        foreach (var water in sprinklers)
        {
            water.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(10);

        foreach (var water in sprinklers)
        {
            water.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(10);

        StartCoroutine(StartWater());

    }
}
