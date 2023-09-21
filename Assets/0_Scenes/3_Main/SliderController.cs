using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Text label;
    float a;
  
    // Start is called before the first frame update
    void Start()
    {
        slider.GetComponent<Slider>().value= PlayerPrefs.GetFloat("SliderValue", 2); 
      label.text=""+ PlayerPrefs.GetFloat("SliderValue", 2);

    }

    public void Changespeed(float speed)
    {
        label.text = "" + (float)Math.Round(speed, 2);
        SaveData(speed);
    }
    void SaveData(float a)
    {
        PlayerPrefs.SetFloat("SliderValue", a);
    }
   
    // Update is called once per frame
    void Update()
    {
        
       
    }
}
