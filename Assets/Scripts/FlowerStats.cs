using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlowerStats : MonoBehaviour {

    private Flower flower;
    public GameObject waterLevel;
    public GameObject growthLevel;
    public GameObject Stats;

    public string Ftarget;

    // Change target when new plant has been clicked
    public void ChangeTarget(Flower target){
        Ftarget = target.id;
        flower = target;
        ShowStats();
    }

    // Display stats UI
    private void ShowStats() {
        Stats.SetActive(true);
    }

    // Hide stats UI    
    public void HideStats(){
        Stats.SetActive(false);
    }

    // Update gauge for water and growth level
    private void Update(){
        if(Stats.activeSelf){
            waterLevel.GetComponent<Image>().fillAmount = Map(flower.water, 0f, 1440f, 0f, 1f);
            growthLevel.GetComponent<Image>().fillAmount = Map(flower.growth, 0f, 2880f, 0f, 1f);
        }
        
    }

    // If water button clicked, call Water() function for the selected flower
    public void Water(){
        flower.Water();
    }

    
    // map function to convert range in another range
    // Input order (5 input) => Actual Value (1) - Initial Range (2) - New Range (2)
    public static float Map(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}


    
