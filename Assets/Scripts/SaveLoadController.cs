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
    private IDataDriver DataDriver = new JsonDataDriver();
    public bool encryption;
    private bool playerChanged;
    private long oldTick;
    private long newTick;


    private void Start()
    {
        SetDefaultPlayers();
        LoadJson();
        playerChanged = false;
        encryption = false;
        Debug.Log(DateTime.UtcNow.Ticks.ToString());
    }

    public void ToggleEncryption(bool EncryptionEnabled)
    {
        encryption = EncryptionEnabled;
    }

    private void UpdateInputField(string headline){
        InputField.text = headline + JsonConvert.SerializeObject(player, Formatting.Indented);
    }

    public void SaveJson(){
        player.lastTick = DateTime.UtcNow.Ticks;
        Debug.Log("Current ticks: "+ DateTime.UtcNow.Ticks);
        if(DataDriver.SaveData("/player-settings.json", player, encryption)){
            Debug.Log("Player successfully saved.");
        }else{
            Debug.LogError("Unable to save file.");
        }
    }

    public void LoadJson(){
        try{
            Player tmpPlayer = DataDriver.LoadData<Player>("/player-settings.json", encryption);
            player = new Player(tmpPlayer.name, tmpPlayer.money, tmpPlayer.crystal, tmpPlayer.level, new Dictionary<string, int>(tmpPlayer.inventory));
            player.lastTick = DateTime.UtcNow.Ticks;
            Debug.Log("Player successfully loaded.");
        }catch(Exception e){
            Debug.LogError($"Unable to load file, giving default values: {e.Message} {e.StackTrace}");
            player = player1;
        }
        UpdateInputField("Player stats:\r\n");
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
        player = new Player("-", 100, 0, 1, new Dictionary<string, int>(){});
        UpdateInputField("Player stats:\r\n");
    }

    private void SetDefaultPlayers(){
        player1 = new Player("Utku Keti", 1200, 24, 3, new Dictionary<string, int>(){
            {"Tomato Seeds", 98},
            {"Corn", 45},
            {"Truck", 1}
        });
        player1.lastTick = DateTime.UtcNow.Ticks;

        player2 = new Player("Lara Croft", 458, 16, 45, new Dictionary<string, int>(){
            {"Shovel", 1},
            {"Berry Seeds", 163},
            {"Pipe", 4},
            {"Fertilizer", 15}
        });
        player2.lastTick = DateTime.UtcNow.Ticks;
    }
}
