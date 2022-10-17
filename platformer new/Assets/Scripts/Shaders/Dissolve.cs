using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private bool isDissolving = false;
    private Material material;
    private float fade = 0f;
    private bool isSolidifying = true;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
 
    void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0;
                isDissolving = false;
            }
            material.SetFloat("_Fade", fade);
        }

        if (isSolidifying)
        {
            fade += Time.deltaTime;
            if (fade >= 1f)
            {
                fade = 1;
                isSolidifying = false;
                //return;
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
