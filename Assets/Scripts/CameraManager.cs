using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {    

    private bool mooving = false;
    private Vector3 endPos;
    private Vector3 startPos;

    // When flower is clicked zoom on the gameObject
    public void FocusTarget(Flower target){
        transform.parent = target.transform;
       
       // Zoom depend of the stage level of the flower
       switch (target.stage){
            case 0:
                endPos = new Vector3(0.5f, 3f, -9f);
                break;
            case 1:
                endPos = new Vector3(0.5f, 4f, -12f);
                break;
            case 2:
                endPos = new Vector3(0f, 6f, -15f);
                break;
       }
       
      
        startPos = new Vector3(0f, 0f, 0f);
        mooving = true;
    }   

    private void Update() {
        if(mooving){
            transform.localPosition = Vector3.Slerp(this.transform.localPosition, endPos, Time.deltaTime * 1f);
        }

        if(Mathf.Round(this.transform.localPosition.x) == endPos.x && Mathf.Round(this.transform.localPosition.y) == endPos.y && Mathf.Round(this.transform.localPosition.z) == endPos.z)
        {
            mooving = false;
        }
    }


    private void OnMouseDrag() {
        
    }
    
}