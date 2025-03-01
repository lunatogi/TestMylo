using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataDriver : IDataDriver
{
    public bool SaveData<T>(string path, T Data, bool Encrypted){
        string pathSum = Application.persistentDataPath + path;

        
        try{
            if(File.Exists(pathSum)){
                Debug.Log("File exist.");
                File.Delete(pathSum);
            }else{
                Debug.Log("File doesn't exist.");
            }
            using FileStream stream = File.Create(pathSum);
            stream.Close();
            File.WriteAllText(pathSum, JsonConvert.SerializeObject(Data));
            return true;
        }catch(Exception e){
            Debug.LogError($"Unable to save: {e.Message} {e.StackTrace}");
            return false;
        }

    }

    public T LoadData<T>(string path, bool Encrypted){
        
        string pathSum = Application.persistentDataPath + path;
        
        try{
            T data;
            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(pathSum));
            return data;
        }catch(Exception e){
            Debug.LogError($"Unable to load: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
