using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropManipHandlersave : MonoBehaviour
{
    public bool moveMode = false;
    public bool fingOnButton = false;
    public float timeSinceDown = 0f;
    public Vector2 firstPos = new Vector2(0, 0);
    public Vector2 tempVec;

    public Interactable interactable;
    //public Draggable draggable;
    public GameObject boundObject;
    public RectTransform gameCanvas;

    public Sprite move;
    public Sprite rotate;

    private void Start()
    {

    }
    void OnEnable()
    {
        Invoke("GetManipObject", 0.1f);
    }

    void GetManipObject()
    {
        interactable = boundObject.GetComponent<Interactable>();
        //draggable = boundObject.GetComponent<Draggable>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 viewOnScreen = Camera.main.WorldToScreenPoint(boundObject.transform.position);
        Vector2 posInWorld = new Vector2(
            viewOnScreen.x,
            viewOnScreen.y);
        transform.position = posInWorld;
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    Vector2 touchPos = Camera.main.ScreenToWorldPoint(.position);

            
        //    timeSinceDown += Time.deltaTime;

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:
        //            if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos))
        //            {
        //                fingOnButton = true;
        //                firstPos = touchPos;
        //                //if ()
        //            }
        //            break;
        //        case TouchPhase.Moved:
        //            // if enough time has elapsed with the 
        //            if (fingOnButton == true && timeSinceDown > 0.2f)
        //            {
        //                if(moveMode == true)
        //                {
        //                    tempVec = new Vector2(touchPos.x - firstPos.x, touchPos.y - firstPos.y);
        //                    draggable.MoveObject(tempVec);
        //                }
        //                else
        //                {
        //                    interactable.TurnObject(touchPos.x - firstPos.x);
        //                }
        //            }
        //            break;
        //        case TouchPhase.Ended:
        //            if (timeSinceDown < 0.2f)
        //            {
        //                toggleMoveRot();
        //            }
        //            else
        //            {
        //                // this smay not be needed
        //            }
        //            timeSinceDown = 0;
        //            break;
        //    }
        //}
    }

    //void OnMouseDown()
    //{
    //    Debug.Log("mouse detected");
    //    GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    //    if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(Input.mousePosition))
    //    {
    //        fingOnButton = true;
    //        firstPos = Input.mousePosition;
    //        //if ()
    //    }
    //}

    //void OnMouseDrag()
    //{
    //    if (fingOnButton == true && timeSinceDown > 0.2f)
    //    {
    //        if (moveMode == true)
    //        {
    //            tempVec = new Vector2(Input.mousePosition.x - firstPos.x, Input.mousePosition.y - firstPos.y);
    //            interactable.MoveObject(tempVec);
    //        }
    //        else
    //        {
    //            interactable.TurnObject(Input.mousePosition.x - firstPos.x);
    //        }
    //    }
    //}

    //void OnMouseExit()
    //{
    //    if (timeSinceDown < 0.2f)
    //    {
    //        ToggleMoveRot();
    //    }
    //    else
    //    {
    //        // this smay not be needed
    //    }
    //    timeSinceDown = 0;
    //}

    public void ToggleMoveRot()
    {
        moveMode = !moveMode;
        if (moveMode == false)
        {
            gameObject.GetComponent<Image>().sprite = rotate;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = move;
        }
        
    }

    
}
