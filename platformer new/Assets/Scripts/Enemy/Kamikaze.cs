using UnityEngine;

public class Kamikaze : Enemy
{
    [SerializeField] private int exploisonDamage;
    [SerializeField] private AudioSource explosionSound;
    private bool isExploided=false;

    public void Exploison(Collision2D collision)
    {
        explosionSound.Play();
        animator.SetTrigger("Explosion");
        collision.gameObject.GetComponent<PlayerCombat>().PlayerTakeDmg(exploisonDamage);

        StartCoroutine(DieCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isExploided)
            Exploison(collision);
    }
}
