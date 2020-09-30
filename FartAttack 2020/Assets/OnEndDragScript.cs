using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnEndDragScript : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    public ChapterSelectManager csm;

    public void OnBeginDrag(PointerEventData data)
    {
        if (csm != null)
        {
            csm.dragging = true;

            //To stop coroutine, since drag has begun, it should stop.
            csm.CheckSlideToSelectedCoroutine();
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (csm != null)
        {
            csm.dragging = false;
        }
    }
}
