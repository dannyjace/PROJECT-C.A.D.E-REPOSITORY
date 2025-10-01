using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Explosives : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    [SerializeField] GameObject DOTitem;
    [SerializeField] ParticleSystem burnEffect;
    [SerializeField] Vector3 burnOffset;

    [SerializeField] int delay;

    private int maxHP;

    void Start()
    {
        maxHP = HP;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        
        if (HP < maxHP)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        burnEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        Instantiate(DOTitem, new Vector3(transform.position.x, .01f, transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }
    
}
