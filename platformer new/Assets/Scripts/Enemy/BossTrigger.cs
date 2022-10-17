using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {
            GameObject.FindGameObjectWithTag("Boss_HealthBar").GetComponent<Canvas>().enabled = true;
            gameObject.SetActive(false);
            boss.SetActive(true);
        }     
    }
}
