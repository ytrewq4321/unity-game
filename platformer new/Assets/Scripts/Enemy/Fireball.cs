using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float liveTime;
    [SerializeField] private AudioSource hitSound;

    public Vector2 Direction;

    public void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position+ Direction, speed * Time.deltaTime);
        StartCoroutine(DestroyObject());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerCombat>().PlayerTakeDmg(damage);
            collider.gameObject.GetComponent<PlayerCombat>().fireballHitSound.Play();
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Ground")
            Destroy(gameObject);
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }
}
