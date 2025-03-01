using System;
using System.Collections.Generic;
using System.Timers;
using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resourceText;
    private int currentResource;
    private int maxResource;
    public int speed;

    DateTime plantTime;
    DateTime currentTime;
    public Resources res = new Resources();
    public TMP_Dropdown ddSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentResource = res.currentQuantitiy;
        maxResource = res.maxCapacity;
        speed = res.speed;
    }

    public void Setup(){
        currentResource = res.currentQuantitiy;
        plantTime = DateTime.Parse(res.plantTime);
        speed = res.speed;
        maxResource = res.maxCapacity;
        

        AddStackedResources();
        
        if(speed == 1){
            ddSpeed.value = 0;
        }else if(speed == 5){
            ddSpeed.value = 1;
        }else if(speed == 30){
            ddSpeed.value = 2;
        }else if(speed == 60){
            ddSpeed.value = 3;
        }else if(speed == 120){
            ddSpeed.value = 4;
        }else{
            ddSpeed.value = 5;
        }
        ddSpeed.RefreshShownValue();
        
        UpdateResourceText();
    }

    // Update is called once per frame
    void Update()
    {
        
        plantTime = DateTime.Parse(res.plantTime);
        speed = res.speed;

        currentTime = DateTime.UtcNow;

        TimeSpan timePast = currentTime - plantTime;
        double timeSec = timePast.TotalSeconds;

        if(timeSec >= speed){
            TimesUp();
        }

    }

    public void AddStackedResources(){
        currentTime = DateTime.UtcNow;

        TimeSpan timePast = currentTime - plantTime;
        double timeSec = timePast.TotalSeconds;

        currentResource += (int)(timeSec / speed);

        if(currentResource > maxResource)
            currentResource = maxResource;

        Debug.Log("timesec current: "+timeSec);
        Debug.Log("updated current: "+currentResource);
        UpdateResourceText();
    }

    public void TimesUp(){
        if((currentResource + 1) <= maxResource){
            currentResource += 1;
            res.currentQuantitiy = currentResource;
            res.plantTime = DateTime.UtcNow.ToString();
            UpdateResourceText();
        }
    }

    public void OnSliderValueChange(int index){
        if(index == 0){
            speed = 1;
        }else if(index == 1){
            speed = 5;
        }else if(index == 2){
            speed = 30;
        }else if(index == 3){
            speed = 60;
        }else if(index == 4){
            speed = 120;
        }else{
            speed = 300;
        }
        res.speed = speed;
    }

    public void ResetResource(){
        currentResource = 0;
        res.currentQuantitiy = currentResource;
        res.plantTime = DateTime.UtcNow.ToString();
        UpdateResourceText();
    }

    public void PlusCapacity(){
        if(maxResource + 5 <= 100){
            maxResource += 5;
            res.maxCapacity = maxResource;
            UpdateResourceText();
        }
    }

    public void MinusCapacity(){
        if(maxResource - 5 >= currentResource){
            maxResource -= 5;
            res.maxCapacity = maxResource;
            UpdateResourceText();
        }
    }

    public void UpdateResourceText(){
        if(currentResource == maxResource)
            resourceText.text = "Resource: "+currentResource+"/"+maxResource+" MAX!";
        else
            resourceText.text = "Resource: "+currentResource+"/"+maxResource;
    }
}
