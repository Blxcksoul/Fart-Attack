using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject button;
    public GameObject currentButton;
    public PropManipHandler propManipHandler = null;
    public bool firstTime;

    public GameObject canvasObj;

    public SphereCollider fanCollider;

    private float distanceFromCamera;

    private void Start()
    {
        // when the fan is active at the beginning of the level, it has all the values set as per needed
        firstTime = true;
        canvasObj = GameObject.Find("Rotate Button Holder");
        Vector3 distanceFromCameraVector = transform.parent.position - Camera.main.transform.position;
        Vector3 linearFromCameraVector = Vector3.Project(distanceFromCameraVector, Camera.main.transform.forward);
        distanceFromCamera = linearFromCameraVector.magnitude;
    }

    // handling rotation from the propmaniphandler
    public void TurnObject(float rotation)
    {
        transform.parent.rotation *= Quaternion.Euler(0, rotation, 0);
    }

    private void Update()
    {

    }

    public void AppearateButton()
    {
        if (firstTime)
        {
            // if the range is entered for the first time, a button is instantiated, and the values 
            // for its propmaniphandler set from here, including telling it which object to follow (i.e. itself)
            firstTime = false;
            currentButton = Instantiate(button);
            propManipHandler = currentButton.GetComponent<PropManipHandler>();
            propManipHandler.boundObject = transform.gameObject;
            propManipHandler.gameCanvas = canvasObj.GetComponent<RectTransform>();
            currentButton.transform.SetParent(canvasObj.transform,false);
        }
        else
        {
            // next time the range is entered, it is simply set to active
            currentButton.SetActive(true);
        }
    }

    public void DisappearateButton()
    {
        // set inactive when leaving the range
        currentButton.SetActive(false);
    }

    // these next two just check for range enter and exit (by the player only)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AppearateButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DisappearateButton();
        }
    }

    //moving the object
    public void MoveObject(Vector2 objPos)
    {
        Vector3 movePos = objPos;
        movePos.z = distanceFromCamera;
        transform.parent.position = Camera.main.ScreenToWorldPoint(movePos);
        transform.parent.position = new Vector3(transform.parent.position.x, 2, transform.parent.position.z);
        //Vector3 movePos;
        //movePos.x = transform.parent.position.x + objPos.x;
        //movePos.y = transform.parent.position.y + objPos.y;
        //movePos.z = distanceFromCamera;
        //transform.parent.position = Camera.main.ScreenToWorldPoint(movePos);
    }
}
