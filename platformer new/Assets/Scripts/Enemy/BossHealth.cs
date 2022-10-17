using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    public UnityEvent Final;
    public UnityEvent HalfFinal;

    private HealthBar healthBar;
    private GameObject boss;
    private bool invoked = false;
    
    void Start()
    {
        healthBar = GameObject.FindWithTag("Boss_HB").GetComponentInChildren<HealthBar>();
        boss = GameObject.FindWithTag("Boss");
    }
    private void Update()
    {
        healthBar.SetHealth(boss.GetComponent<Enemy>().Health.Health);

        if (boss.GetComponent<Enemy>().Health.Health == 100 && !invoked)
        {
            HalfFinal.Invoke();
            invoked = true;
        }
            
        if (boss.GetComponent<Enemy>().Health.Health <= 0)
        {
            GameObject.Find("Enemy").SetActive(false);
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<Enemy>().Die();
            Destroy(GameObject.FindWithTag("Boss_HealthBar"));
            Final.Invoke();
            Destroy(gameObject);
        }  
    }
}
