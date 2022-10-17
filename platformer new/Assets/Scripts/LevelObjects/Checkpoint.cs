using UnityEngine;

public  class Checkpoint : MonoBehaviour
{
    private SpriteRenderer fire;
    private bool visited;

    private void Start()
    {
        fire = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player" && !visited)
        {
            fire.enabled = true;
            visited = true;
            other.GetComponent<PlayerCombat>().PlayerHeal(other.GetComponent<PlayerCombat>().playerHealth.MaxHealth);
            GameManager.LastCheckpoint = transform.position;
        }
    }
}
