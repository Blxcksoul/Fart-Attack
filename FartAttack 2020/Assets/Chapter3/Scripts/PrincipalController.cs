using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrincipalController : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    Vector3 playerPos;
    GameObject playerObject;

    public float enemySpeed;

    bool engaged;
    float engagedTime;
    public EngagedTimerController engagedTimerController;
    bool timerCalled = false;

    public bool stunned = false;
    public GameObject stunParticle;

    public BoyHealth boyHealth;
    public Animator animator;

    bool chasing;

    GameObject book;

    bool pathPending;

    bool firedBook;
    bool firingBook;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        //Placeholder Animator
        animator = GameObject.Find("Principal").GetComponent<Animator>();

        boyHealth = GameObject.Find("BOY").GetComponent<BoyHealth>();
        //Normal Enemy Speed
        enemySpeed = 1.5f;
        agent.speed = enemySpeed;

        rb.isKinematic = true;

        chasing = false;
        
        book = (GameObject)Resources.Load("Prefabs/Book", typeof(GameObject));

        firedBook = false;
    }

    void FixedUpdate()
    {
        playerPos = playerObject.transform.position;

        DetectPlayer();

        if (stunned)
        {
            agent.speed = 0;
        }
        if (engaged)
        {
            engagedTime += Time.deltaTime;
        }

        if (chasing)
        {
            Chase();
            if (firingBook)
            {
                animator.SetBool("IsWalking", false);
            }
            else if (!firingBook)
            {
                animator.SetBool("IsWalking", true);
            }
        }
        if (!chasing)
        {
            animator.SetBool("IsIdling", true);
        }
        if (chasing && !firedBook)
        {
            StartCoroutine(BookAttack());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Peeled Banana"))
        {
            if (!stunned)
            {
                StartCoroutine(Stun(4));
                Instantiate(stunParticle, transform.TransformPoint(0f, 1.4f, 0f), Quaternion.Euler(90f, 0f, 0f));
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Collided with Player Tag");
            boyHealth.TakeDamage(1);
        }
    }

    void Chase()
    {
        Debug.Log("Chase method called");

        if (!timerCalled)
        {
            engagedTimerController.transform.parent.gameObject.SetActive(true);
            engagedTimerController.engagedBegin = true;
            timerCalled = true;
        }

        if (!pathPending)
        {
            pathPending = true;
            Debug.Log("Finding new place to wander to");
            //Sets NavMeshAgent's destination to a random valid location within 20 units
            agent.destination = RandomNavmeshLocation(20);
        }

        if (agent.remainingDistance < 2)
        {
            pathPending = false;
        }
    }

    IEnumerator Stun(float stunDuration)
    {
        stunned = true;
        //animator.SetBool("IsWalking", false);
        //animator.SetBool("IsStunned", true);
        agent.speed = 0;
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
        //animator.SetBool("IsStunned", false);
        //animator.SetBool("IsWalking", true);
        agent.speed = enemySpeed;
    }

    void DetectPlayer()
    {
        RaycastHit hit;

        Vector3 direction = playerPos - transform.position;

        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 5f))
        {
            if (hit.collider.gameObject == playerObject)
            {
                Debug.Log("Player Detected");
                chasing = true;
            }
        }
    }

    IEnumerator BookAttack()
    {
        firingBook = true;
        animator.SetTrigger("Attack");
        transform.LookAt(playerObject.transform);
        FireBook();
        firedBook = true;
        yield return new WaitForSeconds(1);
        firingBook = false;
        yield return new WaitForSeconds(4);
        firedBook = false;
    }

    void FireBook()
    {
        Instantiate(book, transform.position + transform.forward, Quaternion.identity);
    }

    //Returns a random valid location within a radius for the NavMeshAgent to set its destination to
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}