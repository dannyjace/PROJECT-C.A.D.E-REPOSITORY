using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, IDamage
{
    [Header ("Attributes")]
    [SerializeField] int HP;
    [SerializeField] float shield;
    [SerializeField] float shieldRegenTime;
    [SerializeField] float shieldRegenRate;
    [SerializeField] float shootRate;
    [SerializeField] int turnSpeed;
    [SerializeField] float FOV;
    [SerializeField] int expValue;

    [Header("Model")]
    [SerializeField] Color HPDamageFlash;
    [SerializeField] Color shieldDamageFlash;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    public NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Transform lookPos;
    [SerializeField] int animTransSpeed;
    public GameObject dropItem;
    public Vector3 dropItemOffset = new Vector3(0, 0, 0);

    public Canvas healthBar;
    public Image healthBarFill;
    public Image shieldBar;

    Animator animator;
    Color colorOrig;
    float shootTimer;
    bool playerInTrigger;
    float angleToPlayer;


    int HPOrig;
    float shieldOrig;
    float shieldTimer;

    bool isDead = false;
    bool seePlayer;

    Vector3 playerPos;
    Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        HPOrig = HP;
        shieldOrig = shield;
        healthBarFill.fillAmount = 1;
        healthBarFill.color = Color.green;
        animator = GetComponent<Animator>();
        
        StartCoroutine(DisplayHPBar(0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) { return; }

        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            playerPos = other.transform.position;
            playerDir = playerPos - transform.position;
            return;
        }

        //if (other.CompareTag("EnvDamage"))
        //{
        //  Damage dmg = other.GetComponent<Damage>();
        //  int ranNum = Random.Range(0, dmg.safetyPos.length)
        //  agent.SetDestination(safetyPos[ranNum]);
        //  StartCoroutine(Wait(dmg.lifespan));
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { playerInTrigger = false; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPos = other.transform.position;
            playerDir = playerPos - transform.position;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }
        Timers();

        if (playerInTrigger)
        {            
            if (CanSeePlayer() && 0 != Time.timeScale)
            {
               
                Movement();
                animator.SetBool("SeePlayer", true);
                Vector3 lookAtPos = new Vector3(playerPos.x, playerPos.y + 1.0f, playerPos.z);

                shootPos.LookAt(lookAtPos);

                if (shootTimer >= shootRate) { Shoot(); }
            }
            else
            {
                animator.SetBool("SeePlayer", true);
            }
        }
    }

    void Timers()
    {

        if (shootTimer < shootRate + 1f) { shootTimer += Time.deltaTime; }
        if (shieldTimer < shieldRegenTime + 1f) { shieldTimer += Time.deltaTime; }
        if (shieldTimer > shieldRegenTime && shield < shieldOrig)
        {
            shield += Time.deltaTime * shieldRegenRate;
            if (shield > shieldOrig) { shield = shieldOrig; }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) { return; }

        shieldTimer = 0;

        if (0 < shield)
        {
            shield -= damage;
        }
        else { HP -= damage; }

        StartCoroutine(flash());
        StartCoroutine(DisplayHPBar(damage));
        if (HP <= 0)
        {
            isDead = true;
            agent.isStopped = true;
            
            if(null != dropItem) { Instantiate(dropItem, dropItemOffset, transform.rotation); }

            if (gameObject != null) { Destroy(gameObject); }
          
        }
        else
        {
            //Add hiding here!!!
        }
    }

    bool CanSeePlayer()
    {
        if (isDead) { return false; }

        angleToPlayer = Vector3.Angle(transform.forward, playerDir);

        RaycastHit see;
        Debug.DrawRay(lookPos.transform.position, playerDir, Color.red);

        if (Physics.Raycast(lookPos.position, playerDir + Vector3.down, out see))
        {
            if (angleToPlayer <= FOV && see.collider.CompareTag("Player"))
            {
                seePlayer = true;
                return true;
            }
        }
        seePlayer = false;
        return false;
    }

    public virtual void Shoot()
    {
        shootTimer = 0;
        animator.SetTrigger("Shoot");
        SoundManager.instance.playEnemyShootSound(shootPos);
    }

    public void CreateBullet()
    {
        Instantiate(bullet, shootPos.position, shootPos.rotation);
    }

    public void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    public virtual void Movement() 
    { 
        agent.SetDestination(playerPos);
        SetAnimLocomotion();
    }

    void SetAnimLocomotion()
    {
        float agentSpeedCurr = agent.velocity.magnitude;
        float animSpeedCurr = animator.GetFloat("Speed");
        animator.SetFloat("Speed", Mathf.Lerp(animSpeedCurr, agentSpeedCurr, Time.deltaTime * animTransSpeed));
    }


    IEnumerator DisplayHPBar(int amount)
    {
        healthBar.gameObject.transform.rotation = Quaternion.LookRotation(playerDir);
        healthBarFill.gameObject.transform.rotation = Quaternion.LookRotation(playerDir);
        shieldBar.gameObject.transform.rotation = Quaternion.LookRotation(playerDir);

        healthBar.gameObject.SetActive(true);

        if (0 < shield) { shieldBar.fillAmount = Mathf.Lerp(((float)shield + amount) / shieldOrig, (float)shield / shieldOrig, 1f); }
        else
        {
            healthBarFill.fillAmount = Mathf.Lerp(((float)HP + amount) / HPOrig, (float)HP / HPOrig, 1f);
            // change color of health bar based on % of health left as a gradient from green to red
            healthBarFill.color = Color.Lerp(Color.red, Color.green, (float)HP / HPOrig);
        }


        yield return new WaitForSeconds(1f);
        healthBar.gameObject.SetActive(false);
    }

    IEnumerator Wait(int time)
    {
        if (CanSeePlayer()) { yield return new WaitForSeconds(0); }
        else { yield return new WaitForSeconds(time); }
    }

    IEnumerator flash()
    {
        if (0 < shield) { model.material.color = shieldDamageFlash; }
        else { model.material.color = HPDamageFlash; }
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

}
