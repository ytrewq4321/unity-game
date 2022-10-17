using System.Collections;
using UnityEngine;

public class ShootableEnemy : Enemy
{
    [SerializeField] private float shootDamage;
    [SerializeField] private float shootRate;
    [SerializeField] private float shootRange;
    [SerializeField] private AudioSource shootSound;

    private Animator animator2;
    private Fireball bullet;
    private Transform target;
    private SpriteRenderer sprite;
    private bool canShoot;
    private Transform leftPoint;
    private Transform rightPoint;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator2 = GetComponent<Animator>();
        bullet = Resources.Load<Fireball>("Fireball");
        target = GameObject.Find("EnemyTrigger").transform;
        canShoot = true;
        leftPoint = transform.Find("leftPoint").transform;
        rightPoint = transform.Find("rightPoint").transform;
    }

    private void Update()
    {
        if (target!=null && Vector2.Distance(transform.position, target.transform.position) < shootRange && canShoot)
        {
            StartCoroutine(Shoot());
        }
            
        leftPoint.gameObject.SetActive(sprite.flipX);
        rightPoint.gameObject.SetActive(!sprite.flipX);
    }

    private IEnumerator Shoot()
    {
        if(target!=null)
        {
            canShoot = false;

            yield return new WaitForSeconds(shootRate);
            animator2.SetTrigger("Shoot");
            shootSound.Play();
            Fireball newBullet = Instantiate(bullet, leftPoint.gameObject.activeSelf ? rightPoint.position : leftPoint.position, bullet.transform.rotation);
            newBullet.Direction = (Vector2)target.position - (Vector2)transform.position;

            canShoot = true;
        } 
    }
}
