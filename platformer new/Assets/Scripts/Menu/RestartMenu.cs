using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public LevelLoader level;
    public void Restart()
    {
        level.LoadLevel(SceneManager.GetActiveScene().buildIndex); 
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
