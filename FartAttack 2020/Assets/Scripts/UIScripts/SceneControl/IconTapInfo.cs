using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconTapInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject infoPanel;

    public bool isOn;
    void Start()
    {
        infoPanel = transform.GetChild(transform.childCount-1).gameObject;
    }

    public void ToggleInfo()
    {
        if (isOn)
        {
            infoPanel.SetActive(false);
        }
        else
        {
            infoPanel.SetActive(true);
        }
        isOn = !isOn;
    }

    public void TurnOff()
    {
        isOn = false;
        infoPanel.SetActive(false);
    }
}
