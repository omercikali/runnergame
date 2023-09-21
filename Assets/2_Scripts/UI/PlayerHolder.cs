using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
//    using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Rendering.LookDev;
    public GameObject[] models;
    public GameObject[] models1;
    // Start is called before the first frame update
    void Start()
    {
    //    PlayerPrefs.DeleteKey("SelectModel");

        foreach (var model in models)
        {
            model.SetActive(false);

        }
           models[PlayerPrefs.GetInt("Hat")].SetActive(true);

        foreach (var model in models1)
        {
            model.SetActive(false);

        }
        models1[PlayerPrefs.GetInt("Weapon")].SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
