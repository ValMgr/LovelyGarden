using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

 

    public static void SaveGame (GameObject[] flowers){

        // Setup savegame file path and binary format
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.ALED";
        FileStream stream = new FileStream(path, FileMode.Create);

        // Get a list of each data object foreach plant in scene
        List<GameData> data = new List<GameData>();
        for(int i=0;i<flowers.Length;i++){
            data.Add(new GameData(flowers[i].GetComponent<Flower>()));
        }
       
        // format and add data to binary file
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Succesfuly saving in " + path);

    }

    public static List<GameData> LoadGame (){
        // Read binary file
        string path = Application.persistentDataPath + "/save.ALED";
        if(File.Exists(path)){
            Debug.Log("Save loaded from " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<GameData> data = formatter.Deserialize(stream) as List<GameData>;
            stream.Close();
            
            // and return list of data
            return data;

        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    
}