using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDrawoutScript : MonoBehaviour
{

    float screenAddRatio;
    // Start is called before the first frame update
    void Start()
    {
        screenAddRatio = 9f / 16f / (Screen.width / (float)Screen.height);
        transform.position = new Vector3(transform.localPosition.x,transform.localPosition.y * screenAddRatio, transform.localPosition.z);
    }
}
