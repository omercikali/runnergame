using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text coinText;
    [SerializeField]
    private Text diamondText;
    public GameObject[] models;
    public GameObject[] models1;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "";
        // PlayerPrefs.SetInt("Coin", 500000);
        foreach (var model in models)
        {
            model.SetActive(false);
           
        }
        coinText.text =""+PlayerPrefs.GetInt("Coin");
        diamondText.text="55"+ PlayerPrefs.GetInt("Diamond");
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
    public void ShopMenu1()
    {
        SceneManager.LoadScene("Shop");

    }

}
