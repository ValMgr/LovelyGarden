using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {    

    private bool mooving = false;
    private Vector3 endPos;
    public Vector3 startPos;

    // When flower is clicked zoom on the gameObject
    public void FocusTarget(Flower target){
        transform.parent = target.transform;
       
       // Zoom depend of the stage level of the flower
       // /!\ Will change with new type of plant
       switch (target.stage){
            case 0:
                endPos = new Vector3(0.5f, 4f, -12f);
                break;
            case 1:
                endPos = new Vector3(0.5f, 4f, -12f);
                break;
            case 2:
                endPos = new Vector3(0f, 6f, -15f);
                break;
       }
      
        // Camera is mooving
        mooving = true;
    }

    // When back button clicked reset view to initial position
    public void ResetFocus(){
        transform.parent = null;
        endPos = startPos;
        GameObject.Find("GameManager").GetComponent<FlowerStats>().HideStats();
    }   

    private void Update() {
        // If mooving, go to plant position
        if(mooving){
            transform.localPosition = Vector3.Slerp(this.transform.localPosition, endPos, Time.deltaTime * 1f);
        }

        // If position reached stop camera mooving
        if(Mathf.Round(this.transform.localPosition.x) == endPos.x && Mathf.Round(this.transform.localPosition.y) == endPos.y && Mathf.Round(this.transform.localPosition.z) == endPos.z)
        {
            mooving = false;
        }
    }
    
}