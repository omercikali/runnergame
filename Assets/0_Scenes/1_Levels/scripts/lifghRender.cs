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
            // I��k kayna��n�n sa�t��� ���k de�erini de�i�tir
            dl.intensity = 1.7f; // �rnek olarak ���k �iddetini 2.0'a ayarlad�k
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
