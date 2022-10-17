using UnityEngine;

[CreateAssetMenu]
public class StompAbility : Ability
{
    int attackForce = 5;
    TrailRenderer tr;
    public override void Activate(GameObject parrent)
    {
        PlayerMovement movement = parrent.GetComponent<PlayerMovement>();

        if(movement.IsJumping&&movement.JumpCount==2)
        {
            Rigidbody2D rb = parrent.GetComponent<Rigidbody2D>();
            Animator animator = parrent.GetComponentInChildren<Animator>();
            Transform stomp = parrent.transform.Find("Stomp");
            tr = stomp.GetComponent<TrailRenderer>();

            Vector2 attackRadius = new Vector2(16f, -100f);
            
            animator.SetTrigger("StompAttack");
            tr.emitting = true;
            rb.AddForce(new Vector2(0, -200), ForceMode2D.Impulse);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(stomp.position, attackRadius, 1f, EnemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().EnemyTakeDmg(attackForce);
            }
        }
    }

    public override void BeginCoolDonw(GameObject parrent)
    {
        tr.emitting = false;
    }
}
