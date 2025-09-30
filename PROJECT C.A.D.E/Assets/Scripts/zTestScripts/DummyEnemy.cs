using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"Target hit! Took {damage} damage.");

        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }
}