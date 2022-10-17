using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWayPointDistance=3f;
    [SerializeField] private SpriteRenderer enemySprite;

    private Animator animator;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("EnemyTrigger").transform;
        enemySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0.5f, 0.2f);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            if(target!=null)
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            this.path = path;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            if (path == null)
                return;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.fixedDeltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }

            enemySprite.flipX = target.position.x > transform.position.x;
        }  
    }
}
