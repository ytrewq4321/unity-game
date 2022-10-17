using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        levelLoader.LoadLevel(0);
        SceneManager.LoadScene(0);
    }
}
