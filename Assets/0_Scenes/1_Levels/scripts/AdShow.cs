using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class AdShow : MonoBehaviour
{
   

   

    // Start is called before the first frame update
    void Start()
    {
       
        

    }
  
    public void ShowRewardedAd()
    {
        UnityRewardedAd.Instace.ShowAd();
    }
}
//  PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin") + 500);
//  public void ShowRewardedAd()
