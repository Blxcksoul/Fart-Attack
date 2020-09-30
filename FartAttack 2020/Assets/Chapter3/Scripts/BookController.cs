using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    Vector3 directionTowardsPlayer;
    Vector3 playerPos;
    GameObject playerObject;
    Rigidbody rb;
    BoyHealth boyHealth;
    bool accelerating;
    bool bookInFlight;

    GameObject enemyObject;
    Vector3 enemyPos;
    Vector3 directionTowardsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        boyHealth = GameObject.Find("BOY").GetComponent<BoyHealth>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObject.transform.position;
        directionTowardsPlayer = playerPos - gameObject.transform.position;
        enemyObject = GameObject.Find("Enemy");

        rb = gameObject.GetComponent<Rigidbody>();

        accelerating = true;
        bookInFlight = false;

        StartCoroutine(BookFlightPath());
    }

    void FixedUpdate()
    {
        enemyPos = enemyObject.transform.position;

        if (accelerating)
        {
            rb.AddForce(directionTowardsPlayer.normalized * 10);
            rb.AddTorque(Vector3.down);
        }
        else if (!accelerating)
        {
            UpdateDirection();
            rb.AddForce(-directionTowardsEnemy.normalized * 20);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with Player Tag");
            boyHealth.TakeDamage(1);
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Enemy") && bookInFlight)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BookFlightPath()
    {
        yield return new WaitForSeconds(1);
        bookInFlight = true;
        accelerating = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void UpdateDirection()
    {
        directionTowardsEnemy = gameObject.transform.position - enemyPos;
    }
}
