using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoorScript : MonoBehaviour
{
    public GameObject doorCollider;
    public float enterTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlockDoor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BlockDoor()
    {
        yield return new WaitForSeconds(enterTime);
        doorCollider.SetActive(true);
    }
}
