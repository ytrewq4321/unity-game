using UnityEngine;

public class Parallax_Y : MonoBehaviour
{
    [SerializeField] private float parallaxFactor;
    private float length;
    private float startPosition;
    private GameObject camera;
   
    void Start()
    {
        camera = GameObject.Find("CM vcam1");
        startPosition = transform.position.y;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = (camera.transform.position.y * (1-parallaxFactor));
        float distance = (camera.transform.position.y * parallaxFactor);
        transform.position = new Vector3(transform.position.x, startPosition + distance, transform.position.z);

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
