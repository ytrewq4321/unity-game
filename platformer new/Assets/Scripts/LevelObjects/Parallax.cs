using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxFactor;
    private float length;
    private float startPosition;
    private GameObject camera;
    
    void Start()
    {
        camera = GameObject.Find("CM vcam1");
        startPosition = transform.position.x;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (camera.transform.position.x * (1-parallaxFactor));
        float distance = (camera.transform.position.x * parallaxFactor);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if(temp>startPosition+ length)
        {
            startPosition += length;
        }
        else if(temp<startPosition-length)
        {
            startPosition -= length;
        }
    }
}
