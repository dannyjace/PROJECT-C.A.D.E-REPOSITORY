using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Soldier : EnemyAI
{
    [SerializeField] float roamRate;
    [SerializeField] float roamDist;

    [SerializeField] Animator animator;
    [SerializeField] int animTransSpeed;
    [SerializeField] int destroyDelay;

    [SerializeField] AudioClip[] footSteps;

    float roamTimer;
    Vector3 startPos;
    float origStop;

    public override void ClassStart()
    {
        startPos = transform.position;
        origStop = agent.stoppingDistance;
        animator = GetComponent<Animator>();
    }
    
    public override void TakeDamage(int dmg)
    {
        if(!isDead)
        {
            base.TakeDamage(dmg);
            animator.SetTrigger("Hit");
        }
    }


    public override void ClassUpdateBegin()
    {
        if (agent.remainingDistance < 0.01f) { roamTimer += Time.deltaTime; }


    }

    public override void ClassUpdateEnd()
    {
        if (!canSeePlayer && !isDead) { CheckRoam(); }
        SetAnimLocomotion();
    }

    public override void ClassDeath()
    {
        animator.SetTrigger("Dead");
    }

    void SetAnimLocomotion()
    {
        float agentSpeedCurr = agent.velocity.magnitude;
        float animSpeedCur = animator.GetFloat("Speed");

        animator.SetFloat("Speed", Mathf.Lerp(animSpeedCur, agentSpeedCurr, Time.deltaTime * animTransSpeed));
    }

    void CheckRoam()
    {
        if (roamTimer >= roamRate && agent.remainingDistance < 0.01f)
        {
            Roam();
        }
    }

    void Roam()
    {
        roamTimer = 0;
        agent.stoppingDistance = 0;

        Vector3 ranPos = Random.insideUnitSphere * roamDist;
        ranPos += startPos;
        NavMeshHit hit;
        NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);
        agent.SetDestination(hit.position);

    }

    public override void Movement(Vector3 playerDir)
    {
        agent.stoppingDistance = origStop;
        agent.SetDestination(GameManager.instance.player.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance) { FaceTarget(); }

    }

    public override void Shoot()
    {
        shootTimer = 0;
        animator.SetTrigger("Shoot");
    }


    public void DestroyThisObject()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        isDead = true;
        agent.isStopped = true;
        //agent.SetDestination(transform.position);

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    public void FootStep() 
    {
        int arrayPos = Random.Range(0, footSteps.Length - 1);

        AudioSource.PlayClipAtPoint(footSteps[arrayPos], transform.position);
    }
}
