using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCounter : MonoBehaviour
{
    public int sofa1Count, sofa2Count, tableCount, plantCount, bearcount;

    public int sofa1Limit, sofa2Limit, tableLimit, plantLimit, bearlimit;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateCounter()
    {
        StartCoroutine(DelayedUpdate());
    }

    public bool CheckIfSpawnable(string tag)
    {
        switch (tag)
        {
            case "Sofa1":
                if(sofa1Count < sofa1Limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Sofa2":
                if (sofa2Count < sofa2Limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Table":
                if (tableCount < tableLimit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Plant":
                if (plantCount < plantLimit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "Teddy Bear":
                if (bearcount < bearlimit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;

        }
    }

    IEnumerator DelayedUpdate()
    {
        yield return new WaitForEndOfFrame();
        sofa1Count = GameObject.FindGameObjectsWithTag("Sofa1").Length;
        sofa2Count = GameObject.FindGameObjectsWithTag("Sofa2").Length;
        tableCount = GameObject.FindGameObjectsWithTag("Table").Length;
        plantCount = GameObject.FindGameObjectsWithTag("Plant").Length;
        bearcount = GameObject.FindGameObjectsWithTag("Teddy Bear").Length;
    }
    
}
