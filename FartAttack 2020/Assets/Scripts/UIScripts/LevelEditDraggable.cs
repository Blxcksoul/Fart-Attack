using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditDraggable : MonoBehaviour
{
    private float distanceFromCamera;

    bool underUI;
    public bool collided;

    public static GameObject interactedObject;
    public GameObject gridChildRed, gridChildGreen;
    public GameObject UiCanvas;
    Vector3 prevPos;

    public FurnitureCounter FurnitureCounter;

    void Start()
    {
        Vector3 distanceFromCameraVector = transform.position - Camera.main.transform.position;
        Vector3 linearFromCameraVector = Vector3.Project(distanceFromCameraVector, Camera.main.transform.forward);
        distanceFromCamera = linearFromCameraVector.magnitude;

        interactedObject = this.gameObject;

        FurnitureCounter = Camera.main.GetComponent<FurnitureCounter>();
        FurnitureCounter.UpdateCounter();
    }

    void Update()
    {
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

        if (IsPointerOverUIObject())
        {
            underUI = true;
        }
        else
        {
            underUI = false;
        }

        if (interactedObject != this.gameObject)
        {
            UiCanvas.SetActive(false);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void OnMouseDrag()
    {
        if (!underUI)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = distanceFromCamera;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(Mathf.Round(transform.position.x) + 0.5f, GetComponent<Renderer>().bounds.size.y/2, Mathf.Round(transform.position.z) + 0.5f);
        }

        if (transform.position.x <= -3.5) transform.position = new Vector3(-3.5f,transform.position.y,transform.position.z);
        if (transform.position.x >= 3.5) transform.position = new Vector3(3.5f, transform.position.y, transform.position.z);
        if (transform.position.z <= -3.5) transform.position = new Vector3(transform.position.x, transform.position.y, -3.5f);
        if (transform.position.z >= 3.5) transform.position = new Vector3(transform.position.x, transform.position.y, 3.5f);
    }

    void OnMouseDown()
    {
        prevPos = transform.position;
        interactedObject = this.gameObject;

        if(interactedObject == this.gameObject)
        {
            UiCanvas.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        if (collided)
        {
            transform.position = prevPos;
        }
    }

    public void TurnObject()
    {
        transform.rotation *= Quaternion.Euler(0, 90, 0);
    }

    public void DeleteObject()
    {
        transform.position = new Vector3(0, -10, 0);
        StartCoroutine(DeleteDelay());
    }
    //Delay so that when destroying object, grid pieces can detect onTriggerExit
    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FurnitureCounter.UpdateCounter();
        Destroy(gameObject);
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
