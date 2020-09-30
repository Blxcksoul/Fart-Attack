using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditDraggableTemp : MonoBehaviour
{
    private float distanceFromCamera;
    public GameObject draggableObject;
    public float xMax, xMin, zMax, zMin;

    bool collided;
    bool objectSpawned = false;

    public GameObject gridChildRed, gridChildGreen;

    public FurnitureCounter FurnitureCounter;

    public string objectTag;

    void Start()
    {
        Vector3 distanceFromCameraVector = transform.position - Camera.main.transform.position;
        Vector3 linearFromCameraVector = Vector3.Project(distanceFromCameraVector, Camera.main.transform.forward);
        distanceFromCamera = linearFromCameraVector.magnitude;

        objectTag = gameObject.tag;
        FurnitureCounter = Camera.main.GetComponent<FurnitureCounter>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(Mathf.Round(transform.position.x) + 0.5f, GetComponent<Renderer>().bounds.size.y / 2, Mathf.Round(transform.position.z) + 0.5f);

        if (Input.GetMouseButtonUp(0))
        {
            CheckDraggablePos();
        }

        if (collided)
        {
            //Do collide feedback
            gridChildRed.SetActive(true);
            gridChildGreen.SetActive(false);
        }
        else
        {
            //Undo
            gridChildRed.SetActive(false);
            gridChildGreen.SetActive(true);
        }
    }

    void CheckDraggablePos()
    {
        if (transform.position.x > xMin && transform.position.x < xMax && transform.position.z > zMin && transform.position.z < zMax)
        {
            if (!collided && !objectSpawned)
            {
                Debug.Log(objectTag);
                if (FurnitureCounter.CheckIfSpawnable(objectTag))
                {
                    InstantiateObject();
                }
            }
        }
        transform.position = new Vector3(0, -10, 0);
        StartCoroutine(DeleteDelay(gameObject));
    }

    //Delay so that when destroying object, grid pieces can detect onTriggerExit
    IEnumerator DeleteDelay(GameObject obj)
    {
        objectSpawned = true;
        yield return new WaitForSecondsRealtime(0.1f);
        FurnitureCounter.UpdateCounter();
        Destroy(obj);
    }

    public void InstantiateObject()
    {
        Instantiate(draggableObject, transform.position, transform.rotation);
    }

    void OnTriggerStay(Collider col)
    {
        if (!col.CompareTag("Ground"))
        {
            collided = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!col.CompareTag("Ground"))
        {
            collided = false;
        }
    }
}
