using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour {

    public GameObject sunLight;
    [HideInInspector]
    public GameObject[] flowers;
    public GameObject menu;

    private void Awake() {

        flowers = GameObject.FindGameObjectsWithTag("Plant");
        // Load savegame 
        List<GameData> data = SaveSystem.LoadGame();
        Debug.Log(data[0].id);
        setLight();

    }

    private void Update() {
        // Display or Hide menu
        if(Input.GetKeyDown("escape")){
            if(menu.activeSelf){
                menu.SetActive(false);
            }
            else{
                menu.SetActive(true);
            }

        }
    }

    private void setLight(){

        // Set sunLight angle depending on the current hour
        int currentHour = System.DateTime.Now.Hour;
        float currentAngle = (currentHour * 360 / 24) - 110;
        sunLight.transform.Rotate(currentAngle, 0, 0, Space.Self);

        if(currentHour < 12){
            RenderSettings.skybox.SetFloat("_Exposure", Map(currentHour, 0f, 12f, 0.15f, 1f));
        }
        if(currentHour >= 12){
            RenderSettings.skybox.SetFloat("_Exposure", Map(currentHour, 12f, 23f, 1f, 0.15f));
        }

    }

    public void CreateNewPlant(){
        
    }

    public void Exit(){
        // onClick function to quit application
        Application.Quit();
    }

    // private void OnApplicationQuit(){
    //     // Save game when application exit
    //     SaveSystem.SaveGame(flowers);

    // }

    private void OnApplicationPause(bool pauseStatus) {
        // Save game when application 'exit' on mobile device (swipe up or exit without button exit)
        SaveSystem.SaveGame(flowers);
    }


    // map function to convert range in another range -- Could create a Class with each function like that but currently there is only this one
    public static float Map(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
 
}