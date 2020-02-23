using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    
    public System.DateTime lastDateTime;
    public float growthLevel;
    public float waterLevel;
    public int stage;

    public GameData (Flower flower){

        // Setup variables to save
        lastDateTime = flower.currentTime;
        growthLevel = flower.growth;
        waterLevel = flower.water;
        stage = flower.stage;
    }

}