using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Vector3 stompSize;
    [SerializeField] private List<AudioSource> attackSound;
    [SerializeField] private GameObject cam;
    [SerializeField] public AudioSource fireballHitSound;

    public UnityEvent Dead;
    private Animator animator;
    private SpriteRenderer sprite;
    private Material blinkMaterial;
    private Material originalMaterial;
    private Transform attackPointLeft;
    private Transform attackPointRight;

    public UnitHealth playerHealth;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float moveHorizontal;
    private Transform stomp;

    void Start()
    {
        playerHealth = new UnitHealth(100, 100);
        attackPointLeft = transform.Find("AttackPointLeft");
        attackPointRight = transform.Find("AttackPointRight");
        
        stomp = GameObject.Find("Stomp").transform;
        stompSize =  new Vector3(16f, -100f, 0f);
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sprite.material;
        blinkMaterial = Resources.Load("BlinkMaterial",typeof(Material)) as Material;
        healthBar = GameObject.FindWithTag("Player_HB").GetComponentInChildren<HealthBar>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        FlipAttackPoint();

        if (Time.time>=nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Attack");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }  
    }

    private void Attack()
    {
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(
            attackPointRight.gameObject.activeSelf? attackPointLeft.position:attackPointRight.position, attackRange, enemyLayers);
        foreach  (Collider2D enemy in hitEnemies)
        {
            attackSound[Random.Range(0,2)].Play();
            enemy.GetComponent<Enemy>().EnemyTakeDmg(damage);
        }    
    }

    private void FlipAttackPoint()
    {
        attackPointRight.gameObject.SetActive(sprite.flipX);
        attackPointLeft.gameObject.SetActive(!sprite.flipX);
    }

    private void OnDrawGizmos()
    {
        if (attackPointRight == null || attackPointLeft == null)
            return;
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireCube(stomp.position, stompSize);
    }

    public void PlayerHeal(int heal)
    {
        playerHealth.HealUnit(heal);
        healthBar.SetHealth(playerHealth.Health);
    }

    public void PlayerTakeDmg(int dmg)
    {
        //animator.SetTrigger("Hurt");
        //sprite.material = blinkMaterial;
        //GameManager.gameManager.PlayerHealth.DmgUnit(dmg);
        //healthBar.SetHealth(GameManager.gameManager.PlayerHealth.Health);
        //if (GameManager.gameManager.PlayerHealth.Health <= 0)
        //    Die();
        //else
        //    Invoke("ResetMaterial", 0.2f);

        animator.SetTrigger("Hurt");
        sprite.material = blinkMaterial;
        playerHealth.DmgUnit(dmg);
        healthBar.SetHealth(playerHealth.Health);
        if (playerHealth.Health <= 0)
            Die();
        else
            Invoke("ResetMaterial", 0.2f);
    }

    public void Die()
    {
        Dead.Invoke();
        Destroy(gameObject);
        //cam.SetActive(false);
        //transform.position = new Vector2(100, -200);
    }

    private void ResetMaterial()
    {
        sprite.material = originalMaterial;
    }
}
