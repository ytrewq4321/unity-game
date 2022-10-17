using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    //public UnitHealth PlayerHealth = new UnitHealth(100, 100);
    public static Vector2 LastCheckpoint;
    public Vector2 StartPosition;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
            Destroy(this);
        else
            gameManager = this;

        if (LastCheckpoint !=Vector2.zero)
            GameObject.Find("Player").transform.position = LastCheckpoint;
        else
            GameObject.Find("Player").transform.position = StartPosition;       
    }
}
