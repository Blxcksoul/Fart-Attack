using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public float fanSpeed = 180f; //The rotation speed of the fan blades in degrees per second
    public Transform fanBlades; //Stores the transform of the seperate fan blades
    public GameObject windHitbox;

    private bool FanState = true; //Stores the On or Off state of the fan, True is on False is Off

    void Start()
    {
        if (FanState == true)
        {
            windHitbox.SetActive(true);
        }
        else
        {
            windHitbox.SetActive(false);
        }
    }

    void Update()
    {
        if(FanState == true)
        {
            fanBlades.Rotate(0, 0, fanSpeed * Time.deltaTime);
        }
    }

    public void ToggleFan()//Toggles the fan off or on
    {
        if(FanState == true)
        {
            FanState = false;
            windHitbox.SetActive(false);
        }
        else
        {
            FanState = true;
            windHitbox.SetActive(true);
        }
    }

    public void FanOn()//Turns fan on
    {
        FanState = true;
        windHitbox.SetActive(true);
    }

    public void FanOff()//Turnsfan off
    {
        FanState = false;
        windHitbox.SetActive(false);
    }
}
