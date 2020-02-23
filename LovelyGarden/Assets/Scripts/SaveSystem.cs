using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveGame (Flower flower){

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.ALED";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(flower);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Succesfuly saving in " + path);

    }

    public static GameData LoadGame (){
        string path = Application.persistentDataPath + "/save.ALED";
        if(File.Exists(path)){
            Debug.Log("Save loaded from " + path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;

        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
}