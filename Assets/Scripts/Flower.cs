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

        // Calling Growth management script
        Growing();

        // Display correct mesh in function of current stage level
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

    // Target Flower when clicked and show plant's stats
    private void OnMouseDown() {
    
        if (!EventSystem.current.IsPointerOverGameObject()){
            // Call FocusTarget method from MainCamera gameobject
            GameObject.Find("MainCamera").GetComponent<CameraManager>().FocusTarget(this);
            GameObject.Find("GameManager").GetComponent<FlowerStats>().ChangeTarget(this);
        }

    }

    /* =================================== PLANT MANAGEMENT ====================================== */

    // Refill water buffer
    public void Water(){
        water = 1440f; // 1440f = 100%;
    }

    // Growth in function of time spent
    private void Growing(){

        int buffer = 0;
        // Foreach minutes spent
        for(int i=0;i<totalMinutes;i++){
            // If water buffer isnt empty
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
        // Remove each minutes used to growth
        // times remaining is each minutes without water in tank
        totalMinutes -= buffer;

        // if the remaining time is higher than 2d (2880 minutes) decrease 1 stage level
        if(totalMinutes > 2880f){
            if(stage > 0){
                stage--;
                growth = 0;
            }
        }
            
    }

    /* =================================== LOAD GAME MANAGEMENT ====================================== */

   

    public void LoadData(GameData data){

        // Check if data.id correspond to current flower's id
        // If yes update data from savegame file
        if(data != null && data.id == id){
            lastTime = data.lastDateTime;
            growth = data.growthLevel;
            water = data.waterLevel;
            stage = data.stage;
            Debug.Log("Last data for object " + data.id + " => " + data.growthLevel + " /2880 growth - " + data.waterLevel + " / 1440 water - " + data.stage + " / 3");
        }
        else{
            Debug.Log("data null or incorrect id");
        }
    }

}
