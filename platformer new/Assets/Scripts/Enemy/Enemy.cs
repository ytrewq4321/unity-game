using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    protected Animator animator;
    public UnitHealth Health;

    [SerializeField] private AudioSource attackSound;
    [SerializeField] private Transform player;
    [SerializeField] private float repulsiveForce;
    [SerializeField] private float attackRate;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;

    private float nextAttackTime = 1f;

    private void Start()
    {
        Health = new UnitHealth(maxHealth, maxHealth);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    public virtual void Attack(Collision2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            if (gameObject.tag == "Hound" || gameObject.tag == "Skeleton")
                attackSound.Play();
            collision.gameObject.GetComponent<PlayerCombat>().PlayerTakeDmg(damage);
        }
    }

    public virtual void EnemyTakeDmg(int dmg)
    {
        Health.DmgUnit(dmg);
        if (player.position.x > transform.position.x)
            rb.AddForce(new Vector2(-repulsiveForce, repulsiveForce / 2), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(repulsiveForce, repulsiveForce / 2), ForceMode2D.Impulse);
        Debug.Log(Health.Health);
        if (Health.Health <= 0)
            Die();
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        StartCoroutine(DieCoroutine());
    }
    
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack(collision);
        }
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    protected IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
