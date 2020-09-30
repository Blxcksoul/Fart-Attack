using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartInteractionScript : MonoBehaviour
{
    public float fartSpeed = 1f; //speed of the fart in units per second

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "FanWind") //checks if the collider belongs to a fan object
        {
            Vector3 fanDirection = other.transform.forward; //finds the direction the fan is pointing in (the fan Z axis)

            transform.Translate(fanDirection * fartSpeed * Time.deltaTime, Space.World); //moves the fart along the fan's Z axis
        }
        else if(other.gameObject.tag == "WindowWind") //checks if the collider belongs to a window object
        {
            Vector3 windowDirection = new Vector3();

            switch (other.GetComponent<WindowScript>().windDirection)
            {
                case 0:
                    break;
                case 1:
                    windowDirection = other.transform.forward; //finds the window's Z direction
                    transform.Translate(windowDirection * fartSpeed * Time.deltaTime, Space.World); //moves the fart along the window's -Z direction
                    break;
                case 2:
                    windowDirection = other.transform.forward * -1; //finds the window's -Z direction
                    transform.Translate(windowDirection * fartSpeed * Time.deltaTime, Space.World); //moves the fart along the window's Z direction
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WindowPane") //checks if the collider belongs to the window pane
        {
            Destroy(gameObject);
        }
    }
}
