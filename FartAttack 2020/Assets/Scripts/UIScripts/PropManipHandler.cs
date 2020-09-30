using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropManipHandler : MonoBehaviour
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
    // Start is called before the first frame update

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
        // moving the button to follow the object
        Vector2 viewOnScreen = Camera.main.WorldToScreenPoint(boundObject.transform.position);
        Vector2 posInWorld = new Vector2(
            viewOnScreen.x,
            viewOnScreen.y);
        transform.position = posInWorld;
        // checking for player input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // timer
            timeSinceDown += Time.deltaTime;

            // checking the touch phase to stagger actions
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // if the touch is on the button, set fingonbutton to true so it is checked even 
                    // if the finger moves off the button (particularly if moving quickly or if rotating)
                    if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        fingOnButton = true;
                        firstPos = touchPos;
                        //if ()
                    }
                    break;
                case TouchPhase.Moved:
                    // if enough time has elapsed with the finger not lifted
                    if (fingOnButton == true && timeSinceDown > 0.2f)
                    {
                        // checking whether to move or rotate
                        if(moveMode == true)
                        {
                            // GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
                            tempVec = new Vector2(touchPos.x - firstPos.x, touchPos.y - firstPos.y);
                            interactable.MoveObject(tempVec);
                        }
                        else
                        {
                            interactable.TurnObject(touchPos.x - firstPos.x);
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    if (timeSinceDown < 0.2f)
                    {
                        // if the time is less than 0.2s (could be changed to fit better) the player only tapped instead of held, so switch mode
                        ToggleMoveRot();
                    }
                    else
                    {
                        // this smay not be needed
                    }
                    timeSinceDown = 0;
                    fingOnButton = false;
                    break;
            }
        }
    }

    public void ToggleMoveRot()
    {
        // mode switching
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
