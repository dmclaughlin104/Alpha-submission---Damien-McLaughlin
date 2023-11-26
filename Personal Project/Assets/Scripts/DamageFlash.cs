using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{

    MeshRenderer meshRenderer;
    Color origColour;
    float flashTime = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColour = meshRenderer.material.color;

        InvokeRepeating("FlashStart", 0,  0.4f);
            
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Flashing()
    {

        //Invoke("FlashStart", 0.3f);

        /*
        for (float repeatTime = pTime; repeatTime > 0; repeatTime -= Time.deltaTime)
        {
            FlashStart();
        }
        */


    }


    public void FlashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
        
    }

    public void FlashStop()
    {
        meshRenderer.material.color = origColour;
    }
}
