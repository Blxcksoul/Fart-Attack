using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public GameObject leftWall, rightWall, upWall, downWall;

    public string leftWallName, rightWallName, upWallName, downWallName;

    float xPos;
    float zPos;

    public float xMin;
    public float xMax;

    public float zMin;
    public float zMax;

    //public bool gameNotEnd = true;

    private void Start()
    {
        leftWall = GameObject.Find(leftWallName);
        rightWall = GameObject.Find(rightWallName);
        upWall = GameObject.Find(upWallName);
        downWall = GameObject.Find(downWallName);

    }

    void Update()
    {
        // possibly wrong diagnosis of bug. leaving in case may be handy in future
        if (true)
        {
            UpdatePosition();
            transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
        }
        
    }

    void UpdatePosition()
    {
        xPos = Mathf.Clamp(target.position.x, leftWall.transform.position.x - xMin, rightWall.transform.position.x - xMax);

        zPos = Mathf.Clamp(target.position.z, downWall.transform.position.z - zMin, upWall.transform.position.z - zMax);
    }
}
