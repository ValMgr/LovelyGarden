using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

 

    public static void SaveGame (GameObject[] flowers){

        //GameObject[] flowers = GameObject.Find("GameManager").GetComponent<GameManager>().flowers;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.ALED";
        FileStream stream = new FileStream(path, FileMode.Create);

        List<GameData> data = new List<GameData>();

        for(int i=0;i<flowers.Length;i++){
            data.Add(new GameData(flowers[i].GetComponent<Flower>()));
        }

        Debug.Log(data);
        
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Succesfuly saving in " + path);

    }

    public static List<GameData> LoadGame (){
        string path = Application.persistentDataPath + "/save.ALED";
        if(File.Exists(path)){
            Debug.Log("Save loaded from " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<GameData> data = formatter.Deserialize(stream) as List<GameData>;
            stream.Close();

            return data;

        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    
}