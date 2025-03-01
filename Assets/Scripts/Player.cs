using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;

[Serializable]
public class Player
{
    public string name;
    public int money;
    public int crystal;
    public int level;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public long lastTick;

    //public Dictionary<>

    public Player(string _name, int _money, int _crystal, int _level, Dictionary<string, int> _inventory){
        /*this.name = "Utku Keti";
        this.money = 1200;
        this.crystal = 24;
        this.level = 3;
        this.inventory = new Dictionary<string, int>(){
            {"Tomato Seeds", 98},
            {"Corn", 45},
            {"Truck", 1}
        };*/

        this.name = _name;
        this.money = _money;
        this.crystal = _crystal;
        this.level = _level;
        this.inventory = _inventory;
        this.lastTick = DateTime.UtcNow.Ticks;
    }

    public Player Clone()
    {
        Player clone = new Player(this.name, this.money, this.crystal, this.level, this.inventory);
        return clone;
    }
}
