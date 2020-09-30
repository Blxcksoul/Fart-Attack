using UnityEngine;
using UnityEngine.AI;

public class Draggable : MonoBehaviour
{
    private float distanceFromCamera;

    void Start()
    {
        Vector3 distanceFromCameraVector = transform.position - Camera.main.transform.position;
        Vector3 linearFromCameraVector = Vector3.Project(distanceFromCameraVector, Camera.main.transform.forward);
        distanceFromCamera = linearFromCameraVector.magnitude;
    }

    void Update()
    {
        //GetComponent<SphereCollider>().isTrigger = false;
        //GetComponent<Rigidbody>().isKinematic = true;
    }

    //void OnMouseDrag()
    //{
    //    if (draggable)
    //    {
    //        Vector3 movePos = Input.movePos;
    //        movePos.z = distanceFromCamera;
    //        transform.position = Camera.main.ScreenToWorldPoint(movePos);
    //        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
    //    }
    //}

    public void MoveObject(Vector2 objPos)
    {
        Vector3 movePos;
        movePos.x = transform.position.x + objPos.x;
        movePos.y = transform.position.y + objPos.y;
        movePos.z = distanceFromCamera;
        transform.parent.position = Camera.main.ScreenToWorldPoint(movePos);
    }
}
