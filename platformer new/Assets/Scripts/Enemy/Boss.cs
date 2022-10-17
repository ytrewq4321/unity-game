using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float attackRange;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] int attackCount;
    [SerializeField] int realoadTime;
    [SerializeField] int speed;

    private Transform position;
    private Animator animator;
    private Fireball fireball;
    private Transform target;
    private Rigidbody2D rb;
    private bool canShoot;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = GameObject.Find("FireballPosition").transform;
        animator = GetComponent<Animator>();
        fireball = Resources.Load<Fireball>("Fireball");
        target = GameObject.Find("Player").transform;
        canShoot = true;
    }

    private void Update()
    {
        if(target != null)
        {
            rb.AddForce(new Vector2(0, new Vector2(0, 32).y - transform.position.y));
            if (count >= 5)
                rb.AddForce(new Vector2((target.position.x - transform.position.x) * speed, 0));

            if (Vector2.Distance(transform.position, target.transform.position) < attackRange && canShoot)
            {
                StartCoroutine(Attack1());
            }
        }
    }

    private IEnumerator Attack1()
    {
        if (count >= attackCount)
        {
            canShoot = false;
            yield return new WaitForSeconds(realoadTime);
            count = 0;
            canShoot = true;
        }
        else
        {
            canShoot = false;

            yield return new WaitForSeconds(timeBtwAttack);
            animator.SetTrigger("Attack");
            attackSound.Play();

            Fireball newFireball = Instantiate(fireball, position.position, fireball.transform.rotation);
            newFireball.Direction = (Vector2)target.position - (Vector2)transform.position;
            
            Fireball newFireball2 = Instantiate(fireball, position.position, fireball.transform.rotation);
            newFireball2.Direction = new Vector2(newFireball.Direction.x + 20f, newFireball.Direction.y);

            Fireball newFireball3 = Instantiate(fireball, position.position, fireball.transform.rotation);
            newFireball3.Direction = new Vector2(newFireball.Direction.x + 40f, newFireball.Direction.y);

            Fireball newFireball4 = Instantiate(fireball, position.position, fireball.transform.rotation);
            newFireball4.Direction = new Vector2(newFireball.Direction.x - 20f, newFireball.Direction.y);

            Fireball newFireball5 = Instantiate(fireball, position.position, fireball.transform.rotation);
            newFireball5.Direction = new Vector2(newFireball.Direction.x - 40f, newFireball.Direction.y);

            count += 1;

            canShoot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
            Physics2D.IgnoreLayerCollision(6, 9);
    }
}
