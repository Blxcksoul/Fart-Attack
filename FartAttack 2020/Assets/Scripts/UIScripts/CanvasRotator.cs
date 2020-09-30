using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{


    void LateUpdate()
    {
        GetComponentInParent<Transform>().LookAt(GetComponentInParent<Transform>().position + Camera.main.transform.rotation * Vector3.forward);
    }
}
