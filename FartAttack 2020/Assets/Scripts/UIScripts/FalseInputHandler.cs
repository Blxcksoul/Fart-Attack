using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FalseInputHandler : MonoBehaviour
{
    //public bool moveMode = false;
    public bool fingOnButton = false;
    public float timeSinceDown = 0f;
    public Vector2 firstPos = new Vector2(0, 0);
    public Vector2 tempVec;

    public Interactable interactable;
    //public Draggable draggable;
    //public GameObject boundObject;
    //public RectTransform gameCanvas;
    public PropManipHandlersave propManipHandlersave;
    // Start is called before the first frame update
    void Start()
    {
        interactable = transform.parent.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("mouse detected");
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        if (GetComponent<SphereCollider>() == Physics2D.OverlapPoint(Input.mousePosition))
        {
            fingOnButton = true;
            firstPos = Input.mousePosition;
            //if ()
        }
    }

    
    void OnMouseDrag()
    {
        timeSinceDown += Time.deltaTime;
        if (fingOnButton == true && timeSinceDown > 0.2f)
        {
            if (propManipHandlersave.moveMode == true)
            {
                tempVec = new Vector2(Input.mousePosition.x - firstPos.x, Input.mousePosition.y - firstPos.y);
                interactable.MoveObject(tempVec);
            }
            else
            {
                interactable.TurnObject(Input.mousePosition.x - firstPos.x);
            }
        }
    }

    void OnMouseExit()
    {
        if (timeSinceDown < 0.2f)
        {
            propManipHandlersave.ToggleMoveRot();
        }
        else
        {
            // this smay not be needed
        }
        timeSinceDown = 0;
        fingOnButton = false;
    }
}
