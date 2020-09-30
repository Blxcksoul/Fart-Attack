using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickResizer : MonoBehaviour
{
    public GameObject handle;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.width * 512 / 1440f);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.width * 512 / 1440f);
        handle.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)Screen.width * 256 / 1440f);
        handle.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.width * 256 / 1440f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
