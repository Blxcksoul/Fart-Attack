using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffectScript : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public float rotationSpeed;
    public float effectTime;

    void Start()
    {
        StartCoroutine(DestroySelf());
    }


    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        Star1.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        Star2.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        Star3.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator DestroySelf()
    {
       yield return new WaitForSeconds(effectTime);
        Destroy(gameObject);
    }
}
