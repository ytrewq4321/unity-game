using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public LevelLoader levelLoader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            GameManager.LastCheckpoint = Vector2.zero;
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }      
    }
}
