using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flower : MonoBehaviour{

    // 3 Mesh possible pour la plante -- A changer sous forme de listes pour un éventuel ajout de niveau
    public Mesh plant_model0;
    public Mesh plant_model1;
    public Mesh plant_model2;

    public string id;
    public float growth {get; private set;} = 0f;
    public float water {get; private set;} = 0f;
    public int stage {get; private set;} = 0;
    public System.DateTime currentTime {get; private set;}
    public System.DateTime lastTime {get; private set;}


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
        // Debug.Log("Temps écoulé: " + totalMinutes + " minute(s)");
        // Debug.Log("Niveau d'eau: " + water);
        // Debug.Log("Progression: " + growth + " / 2880");
        // Debug.Log("Niveau de plante: " + stage);

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

    // Target Flower when clicked
    private void OnMouseDown() {
    
        if (!EventSystem.current.IsPointerOverGameObject()){
            // Call FocusTarget method from MainCamera gameobject
            GameObject.Find("MainCamera").GetComponent<CameraManager>().FocusTarget(this);
            GameObject.Find("GameManager").GetComponent<FlowerStats>().ChangeTarget(this);
        }

    }

    public void Water(){
        water = 1440f; // 1440f = 100%;
    }

    private void Growing(){

        int buffer = 0;
        // Foreach minutes spent
        for(int i=0;i<totalMinutes;i++){
            // If water tank isnt empty
            if(water > 0f){
                // remove -1f water for each minutes
                water--;
                // Add 1 minute to buffer
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

   

    // public void LoadGame(){
    //     // Load game and set new value from save file
    //     //GameData data = SaveSystem.LoadGame();

    //     if(data != null){
    //         lastTime = data.lastDateTime;
    //         growth = data.growthLevel;
    //         water = data.waterLevel;
    //         stage = data.stage;
    //     }
    // }

}
