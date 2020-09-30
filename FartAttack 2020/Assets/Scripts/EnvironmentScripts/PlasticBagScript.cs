using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBagScript : MonoBehaviour
{
    public GameObject storedFart;
    Vector3 spawnPos;

    private void Awake()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                spawnPos = transform.position + new Vector3(0f, 0f, 0.1f);
                Instantiate(storedFart, spawnPos, Quaternion.identity);
                GameObject secondfart = Instantiate(storedFart, spawnPos, Quaternion.identity);
                secondfart.GetComponent<Rigidbody>().AddForce(-transform.forward * 20f);

                Destroy(gameObject);
            }
        }
    }

}
