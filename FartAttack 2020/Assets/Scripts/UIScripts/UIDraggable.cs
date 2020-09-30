using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDraggable : MonoBehaviour
{
    public GameObject tempDraggable;
    Vector3 spawnObjectPos;

    public void InstantiateTemp()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            spawnObjectPos = hit.point;
            spawnObjectPos = new Vector3(Mathf.Round(spawnObjectPos.x) + 0.5f, tempDraggable.GetComponent<Renderer>().bounds.size.y / 2, Mathf.Round(spawnObjectPos.z) + 0.5f);
        }
        Instantiate(tempDraggable, spawnObjectPos, transform.rotation);
    }
}
