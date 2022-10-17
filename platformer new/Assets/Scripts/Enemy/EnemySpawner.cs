using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private float enemyInterval;
    [SerializeField] private int enemyCount;
    [SerializeField] private bool endless;

    private int count = 0;
    private bool visited = false;

    public virtual IEnumerator SpawnEnemy(GameObject enemy, float interval, int enemyCount)
    {
        if (endless)
        {
            CreateEnemy();
            yield return new WaitForSeconds(interval);
            StartCoroutine(SpawnEnemy(enemy,interval,enemyCount));
        }
        else
        {
            while (count < enemyCount)
            {
                CreateEnemy();
                count++;
                yield return new WaitForSeconds(interval);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyTrigger" && !visited)
        {
            visited = true;
            StartCoroutine(SpawnEnemy(enemy, enemyInterval, enemyCount));
        }
    }

    private void CreateEnemy()
    {
        float x = transform.position.x + Random.Range(-20f, 20f);
        if (x == player.transform.position.x)
            x += 30f;
        GameObject newEnemy = Instantiate(enemy, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }
}
