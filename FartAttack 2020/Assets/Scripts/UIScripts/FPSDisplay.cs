using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;

    public void Start()
    {
        Application.targetFrameRate = 300;
    }

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
    // this code was provided by Rukas90 and Stardog on the Unity Forums and will be removed when it has outlived purpose
}
