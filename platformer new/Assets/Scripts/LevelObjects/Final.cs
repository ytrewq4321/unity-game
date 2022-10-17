using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Cinemachine;

public  class Final : MonoBehaviour
{
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private AudioSource crashSound2;

    private CinemachineVirtualCamera camera;
    private CinemachineBasicMultiChannelPerlin shake;

    private static bool bossDead=false;
    private static bool halfHP = false;

    private void Start()
    {
        camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        shake = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Update()
    {
        if (halfHP)
        {
            RoofCrash(GameObject.Find("Roof1").transform);
            StartCoroutine(CameraShake(4f));
        }
            
        if (bossDead)
        {
            StartCoroutine(CameraShake(10f));
            StartCoroutine(Fall());
        }   
    }

    public void IsHalfBossHP()
    {
        crashSound2.Play();
        halfHP = true;
    }

    public void IsBossDead()
    {
        GameObject.Find("Music").SetActive(false);
        crashSound.PlayDelayed(3f);
        bossDead = true;
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(7f);
        RoofCrash(transform);
        yield return new WaitForSeconds(3f);
        GameObject.Find("BlackScreen").GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(6f);
        bossDead = false;
        SceneManager.LoadScene(0);  
    }

    private void RoofCrash(Transform transform1)
    {
        transform1.position = Vector2.MoveTowards(transform1.position, new Vector2(0, -50), 40 * Time.deltaTime);
    }

    private IEnumerator CameraShake(float time)
    {
        shake.m_FrequencyGain = 3f;
        shake.m_AmplitudeGain = 3f;
        yield return new WaitForSeconds(time);
        shake.m_FrequencyGain = 0f;
        shake.m_AmplitudeGain = 0f;
        halfHP = false;
    }
}


