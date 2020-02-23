using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour {

    public GameObject sunLight;
    public Flower flower;

    public GameObject menu;
    public GameObject flowerStat;

    private void Awake() {
        // Load savegame 
        flower.LoadGame();
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

        // Display flower's stats when selected
        if(flower.selected){
            flowerStat.SetActive(true);
        }
        else{
            flowerStat.SetActive(false);
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

    public void Exit(){
        // onClick function to quit application
        Application.Quit();
    }

    private void OnApplicationQuit(){
        // Save game when application exit
        flower.GetComponent<Flower>().SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus) {
        // Save game when application 'exit' on mobile device (swipe up or exit without button exit)
        flower.GetComponent<Flower>().SaveGame();
    }


    // map function to convert range in another range -- Could create a Class with each function like that but currently there is only this one
    public static float Map(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
}