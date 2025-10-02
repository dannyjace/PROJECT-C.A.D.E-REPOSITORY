using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, IDamage
{

    [SerializeField] int HP;
    [SerializeField] float shield;
    [SerializeField] float shieldRegenTime;
    [SerializeField] float shieldRegenRate;
    [SerializeField] public float shootRate;
    [SerializeField] public int turnSpeed;
    [SerializeField] float FOV;
    [SerializeField] int expValue;

    [SerializeField] Color HPDamageFlash;
    [SerializeField] Color shieldDamageFlash;
    [SerializeField] public Transform shootPos;
    [SerializeField] public GameObject bullet;
    public NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Transform lookPos;

    public Canvas healthBar;
    public Image healthBarFill;
    public Image shieldBar;

    [SerializeField] bool isBoss = false;
    public GameObject dropItem;
    public Vector3 dropItemOffset = new Vector3(0, 0, 0);

    Color colorOrig;
    [HideInInspector] public float shootTimer;
    bool playerInTrigger;
    float angleToPlayer;


    int HPOrig;
    float shieldOrig;
    float shieldTimer;

    [HideInInspector] public bool canSeePlayer;

    [HideInInspector] public bool isDead = false;

    [HideInInspector] public Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        HUDManager.instance.updateGameGoal(1);
        HPOrig = HP;
        shieldOrig = shield;
        if (!isBoss)
        {
            healthBarFill.fillAmount = 1;
            healthBarFill.color = Color.green;
        }
        StartCoroutine(DisplayHPBar(0));
        ClassStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        ClassUpdateBegin();
        if (shootTimer < shootRate + 1f) { shootTimer += Time.deltaTime; }
        if (shieldTimer < shieldRegenTime + 1f) { shieldTimer += Time.deltaTime; }
        if (shieldTimer > shieldRegenTime && shield < shieldOrig)
        {
            shield += Time.deltaTime * shieldRegenRate;
            if (shield > shieldOrig) { shield = shieldOrig; }
        }

        if (playerInTrigger)
        {
            if (canSeePlayer = CanSeePlayer() && 0 != Time.timeScale)
            {
                playerDir = GameManager.instance.player.transform.position - transform.position;
                Movement(playerDir);

                Vector3 playerPos = GameManager.instance.player.transform.position;
                Vector3 lookAtPos = new Vector3(playerPos.x, playerPos.y + 1.0f, playerPos.z);

                shootPos.LookAt(lookAtPos);

                if (shootTimer >= shootRate) { Shoot(); }
            }
        }

        ClassUpdateEnd();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) { return; }

        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            return;
        }

        if (other.CompareTag("EnvDamage"))
        {
            Damage dmg = other.GetComponent<Damage>();
            //int ranNum = Random.Range(0, dmg.safetyPos.length)
            //agent.SetDestination(safetyPos[ranNum]);
            //StartCoroutine(Wait(dmg.lifespan));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { playerInTrigger = false; }
    }

    public virtual void ClassStart() { }

    public virtual void ClassUpdateBegin() { }

    public virtual void ClassUpdateEnd() { }

    public virtual void ClassDeath() { }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) { return; }

        shieldTimer = 0;
        playerDir = GameManager.instance.player.transform.position - transform.position;

        if (0 < shield)
        {
            shield -= damage;
        }
        else { HP -= damage; }

        StartCoroutine(flash());
        StartCoroutine(DisplayHPBar(damage));
        if (HP <= 0)
        {
            agent.SetDestination(transform.position);
            HUDManager.instance.updateGameGoal(-1);
            if (isBoss)
            {
                Instantiate(dropItem, transform.position + dropItemOffset, transform.rotation);
                HUDManager.instance.bossHPBar.gameObject.SetActive(false);
            }

            ClassDeath();
            if (gameObject != null) { Destroy(gameObject); }
            GameManager.instance.playerScript.AttributeController.AddExperience(expValue);
        }
        else
        {
            FaceTarget();
            agent.SetDestination(GameManager.instance.player.transform.position);
        }
    }

    bool CanSeePlayer()
    {
        if (isDead) { return false; }

        playerDir = GameManager.instance.player.transform.position - lookPos.position;
        angleToPlayer = Vector3.Angle(transform.forward, playerDir);

        RaycastHit see;
        Debug.DrawRay(lookPos.transform.position, playerDir, Color.red);

        if (Physics.Raycast(lookPos.position, playerDir + Vector3.up, out see))
        {
            if (angleToPlayer <= FOV && see.collider.CompareTag("Player"))
            {
                //canSeePlayer = true;
                return true;
            }
        }
        //canSeePlayer = false;

        return false;
    }

    IEnumerator flash()
    {
        if (0 < shield) { model.material.color = shieldDamageFlash; }
        else { model.material.color = HPDamageFlash; }
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

    public virtual void Shoot()
    {
        shootTimer = 0;
        CreateBullet();
        //Quaternion shootRot = Quaternion.LookRotation(new Vector3(playerDir.x, shootPos.position.y, playerDir.z));

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

    public virtual void Movement(Vector3 playerDir) { }


    IEnumerator DisplayHPBar(int amount)
    {
        if (!isBoss)
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
        else if (isBoss)
        {
            HUDManager.instance.bossHPBar.gameObject.SetActive(true);
            HUDManager.instance.bossHPBarFill.fillAmount = Mathf.Lerp(((float)HP + amount) / HPOrig, (float)HP / HPOrig, 1f);
            HUDManager.instance.bossHPBarFill.color = Color.Lerp(Color.red, Color.green, (float)HP / HPOrig);
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator Wait(int time)
    {
        if (CanSeePlayer()) { yield return new WaitForSeconds(0); }
        else { yield return new WaitForSeconds(time); }
    }

}
