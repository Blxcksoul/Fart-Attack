using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRatioController : MonoBehaviour
{
    public float actualHWRatio;
    // Start is called before the first frame update
    void Start()
    {
        actualHWRatio = Screen.height / Screen.width;
        if (actualHWRatio > 16 / 9)
        {
            Screen.SetResolution(Screen.width, (Screen.width * 16 / 9), false);
        }
    }
}
