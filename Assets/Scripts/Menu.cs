using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

    public Flower flower;
    public GameObject waterLevel;
    public GameObject growthLevel;

    private void Update() {
        waterLevel.GetComponent<Image>().fillAmount = Map(flower.water, 0f, 2880f, 0f, 1f);
        growthLevel.GetComponent<Image>().fillAmount = Map(flower.growth, 0f, 2880f, 0f, 1f);
    }

    
    // map function to convert range in another range
    public static float Map(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}


    
