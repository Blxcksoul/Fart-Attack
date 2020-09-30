using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditRotate : MonoBehaviour
{
    Transform canvTran;

    void Start()
    {
        canvTran = gameObject.transform.parent;
    }

    void LateUpdate()
    {
        canvTran.LookAt(canvTran.position + Camera.main.transform.rotation * Vector3.forward);
        transform.position = transform.position;
    }
}
