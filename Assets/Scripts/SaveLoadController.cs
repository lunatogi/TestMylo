using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class SaveLoadController : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField InputField;

    [SerializeField]
    private Player player;
    [SerializeField]
    private Player player1;
    [SerializeField]
    private Player player2;
    private ResourceController resController;
    private IDataDriver DataDriver = new JsonDataDriver();
    public bool encryption;
    private bool playerChanged;
    private DateTime LastTime;
    private DateTime CurrTime;


    private void Start()
    {
        resController = GetComponent<ResourceController>();
        SetDefaultPlayers();
        LoadJson();
        playerChanged = false;
        encryption = false;
        
        //CalculateTimeInterval();
    }

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        encryption = EncryptionEnabled;
    }

    private void UpdateInputField(string headline){
        InputField.text = headline + JsonConvert.SerializeObject(player, Formatting.Indented);
    }

    public void SaveJson(){
        SavePlayerData();
        SaveResourceData();
    }

    private void SavePlayerData(){
        //CalculateTimeInterval();
        player.lastTime = DateTime.UtcNow.ToString();
        if(DataDriver.SaveData("/player-settings.json", player, encryption)){
            Debug.Log("Player successfully saved.");
        }else{
            Debug.LogError("Unable to save player.");
        }
    }

    private void SaveResourceData(){
        Resources res = resController.res.Clone();
        if(DataDriver.SaveData("/resource-settings.json", res, encryption)){
            Debug.Log("Source successfully saved.");
        }else{
            Debug.LogError("Unable to save resource.");
        }
    }

    public void LoadJson(){
        LoadPlayerData();
        LoadResourceData();
    }

    private void LoadPlayerData(){
        try{
            Player tmpPlayer = DataDriver.LoadData<Player>("/player-settings.json", encryption);
            player = new Player(tmpPlayer.name, tmpPlayer.money, tmpPlayer.crystal, tmpPlayer.level, new Dictionary<string, int>(tmpPlayer.inventory));
            player.lastTime = tmpPlayer.lastTime;
            CalculateTimeInterval();
            Debug.Log("Player successfully loaded.");
        }catch(Exception e){
            Debug.LogError($"Unable to load player, giving default values: {e.Message} {e.StackTrace}");
            player = player1.Clone();
        }
        UpdateInputField("Player stats:\r\n");
    }

    private void LoadResourceData(){
        try{
            Resources tmpResource = DataDriver.LoadData<Resources>("/resource-settings.json", encryption);
            //player = new Player(tmpPlayer.name, tmpPlayer.money, tmpPlayer.crystal, tmpPlayer.level, new Dictionary<string, int>(tmpPlayer.inventory));
            Debug.Log("Resources : "+tmpResource.currentQuantitiy);
            resController.res = tmpResource.Clone();
            resController.Setup();
            //player.lastTime = tmpPlayer.lastTime;
            //CalculateTimeInterval();
            Debug.Log("Resources successfully loaded.");
        }catch(Exception e){
            Debug.LogError($"Unable to load resource, giving default values: {e.Message} {e.StackTrace}");
        }
    }

    private void CalculateTimeInterval(){
        
        LastTime = DateTime.Parse(player.lastTime);
        Debug.Log("raw last time"+player.lastTime.ToString());
        CurrTime = DateTime.UtcNow;
        Debug.Log("cur time"+DateTime.UtcNow.ToString());
        Debug.Log("last time"+player.lastTime.ToString());
        TimeSpan interval = CurrTime - LastTime;
        Debug.Log("Interval in seconds:" + interval.TotalSeconds);
    }

    public void ToggleCharacter(){
        if(!playerChanged){
            playerChanged = true;
            player = player2.Clone();
            Debug.Log("Player changed to 2");
        }else{
            playerChanged = false;
            player = player1.Clone();
            Debug.Log("Player changed to 1");
        }
        UpdateInputField("Player stats:\r\n");
    }

    public void ClearData(){
        Debug.Log("Character deleted");
        string path = Application.persistentDataPath + "/player-settings.json";
        if(File.Exists(path)){
            File.Delete(path);
        }
        path = Application.persistentDataPath + "/resource-settings.json";
        if(File.Exists(path)){
            File.Delete(path);
        }
        player = new Player("-", 100, 0, 1, new Dictionary<string, int>(){});
        resController.res = new Resources();
        resController.Setup();
        UpdateInputField("Player stats:\r\n");
    }

    private void SetDefaultPlayers(){
        player1 = new Player("Utku Keti", 1200, 24, 3, new Dictionary<string, int>(){
            {"Tomato Seeds", 98},
            {"Corn", 45},
            {"Truck", 1}
        });
        //player1.lastTime = DateTime.UtcNow.ToString();

        player2 = new Player("Lara Croft", 458, 16, 45, new Dictionary<string, int>(){
            {"Shovel", 1},
            {"Berry Seeds", 163},
            {"Pipe", 4},
            {"Fertilizer", 15}
        });
        //player2.lastTime = DateTime.UtcNow.ToString();
    }
}
