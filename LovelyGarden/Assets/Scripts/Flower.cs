using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour{

    // 3 Mesh possible pour la plante -- A changer sous forme de listes pour un éventuel ajout de niveau
    public Mesh plant_model0;
    public Mesh plant_model1;
    public Mesh plant_model2;


    public float growth {get; private set;} = 0f;
    public float water {get; private set;} = 0f;
    public int stage {get; private set;} = 0;
    public System.DateTime currentTime {get; private set;}
    public System.DateTime lastTime {get; private set;}

    public bool selected {get; private set;} = false;
    
    private float totalMinutes;
    private void Start() {
        // Get current time
        currentTime = System.DateTime.Now;

        // Get time spen between 2 connection in minutes
        System.TimeSpan timeSpant = currentTime - lastTime;
        totalMinutes = (float)timeSpant.TotalMinutes;
        totalMinutes = Mathf.Floor(totalMinutes);

        Growing();

        // Display current stats of the games
        Debug.Log("Temps écoulé: " + totalMinutes + " minute(s)");
        Debug.Log("Niveau d'eau: " + water);
        Debug.Log("Progression: " + growth + " / 2880");
        Debug.Log("Niveau de plante: " + stage);

        switch (stage){
            case 0:
                this.GetComponent<MeshFilter>().mesh = plant_model0;
                break;
            case 1:
                this.GetComponent<MeshFilter>().mesh = plant_model1;
                break;
            case 2:
                this.GetComponent<MeshFilter>().mesh = plant_model2;
                break;            
        }

    }

    private void OnMouseDown() {
        // Call FocusTarget method from MainCamera gameobject
        GameObject.Find("MainCamera").GetComponent<CameraManager>().FocusTarget(this);
        selected = true;
    }

    public void Water(){
        water = 2880f; // 2880f = 100%;
    }

    private void Growing(){

        int buffer = 0;
        for(int i=0;i<totalMinutes;i++){
            if(water > 0f){
                water--;
                buffer++;
                if(growth < 2880f){
                    growth++;
                }
                else{
                    if(stage < 2){
                        growth = 0f;
                        stage++;
                    }
                }
            }
        }
        totalMinutes -= buffer;


        if(totalMinutes > 2880f){
            if(stage > 0){
                stage--;
                growth = 0;
            }
        }
            
    }



    /* =================================== SAVE GAME MANAGEMENT ====================================== */

    public void SaveGame(){
        // Save game
        SaveSystem.SaveGame(this);
    }

    public void LoadGame(){
        // Loag game and set new value from save file
        GameData data = SaveSystem.LoadGame();

        lastTime = data.lastDateTime;
        growth = data.growthLevel;
        water = data.waterLevel;
        stage = data.stage;

    }

}
