using System;
using UnityEngine;

public class Resources
{
    public string name;
    public int speed;
    public int maxCapacity;
    public int currentQuantitiy;
    public string plantTime;
    
    public Resources(){
        this.name = "Tomato";
        this.speed = 1;
        this.maxCapacity = 5;
        this.currentQuantitiy = 0;
        this.plantTime = DateTime.UtcNow.ToString();
    }

    public void ResetPlantTime(){
        this.plantTime = DateTime.UtcNow.ToString();
    }

    public Resources Clone()
    {
        Resources clone = new Resources();
        clone.name = this.name;
        clone.speed = this.speed;
        clone.maxCapacity = this.maxCapacity;
        clone.currentQuantitiy = this.currentQuantitiy;
        clone.plantTime = this.plantTime;
        return clone;
    }
}
