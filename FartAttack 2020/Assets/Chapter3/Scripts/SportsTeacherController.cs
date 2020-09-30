using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Sports teacher should behave in the same way as the granny, but moves faster and dashes every 5 seconds to try and catch the player.
public class SportsTeacherController : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    Vector3 playerPos;
    GameObject playerObject;

    public int enemySpeed;

    bool engaged;
    float engagedTime;
    public EngagedTimerController engagedTimerController;
    bool timerCalled = false;

    public float fieldOfViewAngle = 180f;

    bool chasing;
    bool patrolling;
    bool dashing;
    bool patrolPending;

    public bool stunned = false;
    public GameObject stunParticle;

    public BoyHealth boyHealth;

    public float dashCooldown;
    Animator animator;
    float attackCooldown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        //Placeholder Animator
        animator = GameObject.Find("SportsTeacher").GetComponent<Animator>();

        boyHealth = GameObject.Find("BOY").GetComponent<BoyHealth>();
        //Normal Enemy Speed
        enemySpeed = 3;
        agent.speed = enemySpeed;

        //for testing, it should start in patrol in final version
        chasing = false;
        dashing = false;
        patrolling = true;
        patrolPending = false;

        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        playerPos = playerObject.transform.position;
        dashCooldown += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (chasing)
        {
            Chase();
        }
        else if (patrolling)
        {
            Patrol();
        }

        if (stunned)
        {
            agent.speed = 0;
        }

        if (engaged)
        {
            engagedTime += Time.deltaTime;
        }
    }

    private void OnCollisionStay(Collision col)
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
            if (attackCooldown > 1)
            {
                boyHealth.TakeDamage(1);
                attackCooldown = 0;
            }
        }
    }

    void Chase()
    {
        if (!timerCalled)
        {
            engagedTimerController.transform.parent.gameObject.SetActive(true);
            engagedTimerController.engagedBegin = true;
            timerCalled = true;
        }

        if (!dashing)
        {
            Debug.Log("Chasing");
            agent.destination = playerPos;
            animator.SetBool("IsWalking", true);
            if (agent.remainingDistance < 5 && !dashing && dashCooldown > 4)
            {
                StartCoroutine(Dash());
                animator.SetBool("IsDashing", false);
            }
        }

        if (dashing)
        {
            animator.SetBool("IsDashing", true);
            animator.SetBool("IsWalking", false);
        }
    }

    void Patrol()
    {
        if (!patrolPending)
        {
            patrolPending = true;
            Debug.Log("Finding new place to wander to");
            //Sets NavMeshAgent's destination to a random valid location within 20 units
            agent.destination = RandomNavmeshLocation(20);
            //Placeholder animations for start of patrol
            animator.SetBool("IsWalking", true);
        }

        if (agent.remainingDistance < 2)
        {
            patrolPending = false;
            //Placeholder animations for end of patrol
            animator.SetBool("IsWalking", false);
        }

        DetectPlayer();
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

    IEnumerator Dash()
    {
        Debug.Log("Dashing");
        dashing = true;
        //Dash Speed
        agent.speed = 8;
        agent.angularSpeed = 0;

        yield return new WaitForSeconds(1);

        dashing = false;
        agent.speed = enemySpeed;
        agent.angularSpeed = 120;

        dashCooldown = 0;
        Debug.Log("Stopped Dashing");
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
        Vector3 direction = playerPos - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 5f))
            {
                if (hit.collider.gameObject == playerObject)
                {
                    Debug.Log("Player Detected");
                    chasing = true;
                    dashing = false;
                    patrolling = false;
                    patrolPending = false;
                }
            }
        }
    }
}