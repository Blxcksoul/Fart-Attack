using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public float windChangeTime = 10f; //Time in seconds between wind changes
    public int windDirection; //integer to store the state the window is in, 0 = no wind, 1 = wind blowing away from window, 2 = wind sucking towards window

    // Start is called before the first frame update
    void Start()
    {
        windDirection = 0;
        StartCoroutine("WindTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WindTimer() //coroutine that chanegs wind direction every 10 seconds
    {
        for ( ; ; )
        {
            yield return new WaitForSeconds(windChangeTime);
            windDirection = Random.Range(0, 4);
        }
    }


}
