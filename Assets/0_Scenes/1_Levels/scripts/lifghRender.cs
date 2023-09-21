using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class lifghRender : MonoBehaviour
{
    Light dl;
    // Start is called before the first frame update
    void Start()
    {
        dl = FindObjectOfType<Light>();

        if (dl != null)
        {
            // Iþýk kaynaðýnýn saçtýðý ýþýk deðerini deðiþtir
            dl.intensity = 1.7f; // Örnek olarak ýþýk þiddetini 2.0'a ayarladýk
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
