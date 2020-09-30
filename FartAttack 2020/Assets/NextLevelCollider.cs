using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelCollider : MonoBehaviour
{
    public SceneControl sceneControl;

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            sceneControl.BearWin();
        }
    }
}
