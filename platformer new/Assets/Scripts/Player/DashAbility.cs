using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    float dashForce = 50f;
    PlayerMovement movement;
    Rigidbody2D rb;
    TrailRenderer tr;

    public override void Activate(GameObject parrent)
    {
        movement = parrent.GetComponent<PlayerMovement>();
        rb = parrent.GetComponent<Rigidbody2D>();
        tr = parrent.GetComponentInChildren<TrailRenderer>();
        float originalGravity = rb.gravityScale;
        tr.emitting = true;
        rb.gravityScale = 0f;
        rb.AddForce(new Vector2(dashForce * movement.MoveHorizontal, 0f), ForceMode2D.Impulse);
    }

    public override void BeginCoolDonw(GameObject parrent)
    {
        tr.emitting = false;
        rb.gravityScale = movement.NormalGravity;
    }
}
