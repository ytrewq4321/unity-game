using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Player")
        {
            collider.GetComponent<PlayerCombat>().Die();
        } 
    }
}
