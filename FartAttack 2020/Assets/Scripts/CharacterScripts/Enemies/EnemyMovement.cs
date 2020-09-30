using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    Rigidbody rb;
    Transform target;

    public EngagedTimerController engagedTimerController;
    bool timerCalled = false;

    public float enemySpeed;

    bool idle;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    bool patrol;
    public float patrolViewRadius;
    [Range(0, 360)]
    public float patrolViewAngle;
    public Transform[] patrolWayPoints;
    int destinationWayPoint = 0;
    Vector3 playerLastSeenPos;
    bool patrolPending;
    float rotateVariable;
    bool patrolRotating;

    bool chase;
    public float chaseViewRadius;
    [Range(0, 360)]
    public float chaseViewAngle;
    float chaseTimer;
    public float chaseCooldown;

    bool engaged;
    float engagedTime;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Animator animator;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        InitIdle();

        StartCoroutine("FindPlayerDelay", 0.2f);

    }
            
    void Update()
    {
        if (chase)
        {
            Chase();
        }
        else if (patrol)
        {
            Patrol();
            CheckSuspicion();
        }
        else if (idle)
        {
            Idle();
        }

        if (stunned)
        {
            enemyAgent.speed = 0;
        }

        if (engaged)
        {
            engagedTime += Time.deltaTime;
        }
    }

    void Idle()
    {

    }

    void Patrol()
    {
        if(enemyAgent.remainingDistance < 1.5f)
        {
            if (!patrolPending)
                StartCoroutine("PatrolToNext");
        }

        if (patrolRotating)
        {
            rotateVariable += Time.deltaTime*2;
            transform.Rotate(0,Mathf.Cos(rotateVariable),0);
            Mathf.Cos(rotateVariable);
            animator.Play("GrannyAlert");
        }
        else
        {
            rotateVariable = 0;
        }
    }

    IEnumerator PatrolToNext()
    {
        patrolPending = true;

        patrolRotating = true;
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(3);

        patrolRotating = false;
        animator.SetBool("IsWalking", true);

        enemyAgent.speed = enemySpeed;

        if(!(playerLastSeenPos == Vector3.zero))
        {
            enemyAgent.destination = playerLastSeenPos;
            playerLastSeenPos = Vector3.zero;
            destinationWayPoint = (destinationWayPoint + 1) % patrolWayPoints.Length;
            patrolPending = false;
            yield break;
        }

        enemyAgent.destination = patrolWayPoints[destinationWayPoint].position;

        destinationWayPoint = (destinationWayPoint + 1) % patrolWayPoints.Length;

        patrolPending = false;
    }

    void Chase()
    {
        chaseTimer += Time.deltaTime;
        enemyAgent.SetDestination(target.transform.position);

        if(chaseTimer > chaseCooldown)
        {
            playerLastSeenPos = target.transform.position;
            InitPatrol();
        }
    }

    void InitIdle()
    {
        idle = true;
        patrol = false;
        chase = false;

        animator.SetBool("IsWalking", false);
    }

    void InitPatrol()
    {
        patrol = true;
        idle = false;
        chase = false;

        rb.isKinematic = true;
        PatrolToNext();

        animator.SetBool("IsWalking", true);
    }

    void InitChase()
    {
        
        if (!timerCalled)
        {
            engagedTimerController.transform.parent.gameObject.SetActive(true);
            engagedTimerController.engagedBegin = true;
            timerCalled = true;
        }
        
        animator.SetBool("IsWalking", true);
        animator.Play("Walking");
        chase = true;
        idle = false;
        patrol = false;

        rb.isKinematic = true;
        chaseTimer = 0;
        enemyAgent.speed = enemySpeed;

        engaged = true;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    IEnumerator FindPlayerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindPlayer();
        yield return StartCoroutine("FindPlayerDelay", 0.2f);
    }

    void FindPlayer()
    {
        if (chase)
        {
            Collider[] playerInRadius = Physics.OverlapSphere(transform.position, patrolViewRadius, targetMask);

            for (int i = 0; i < playerInRadius.Length; i++)
            {
                if (playerInRadius[i].CompareTag("Player"))
                {
                    chaseTimer = 0;
                }
            }
        }
        else if (patrol)
        {
            Collider[] playerInRadius = Physics.OverlapSphere(transform.position, patrolViewRadius, targetMask);

            for (int i = 0; i < playerInRadius.Length; i++)
            {
                if (playerInRadius[i].CompareTag("Player"))
                {
                    Vector3 dirToPlayer = (target.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, dirToPlayer) < patrolViewAngle / 2)
                    {
                        float distToTarget = Vector3.Distance(transform.position, target.position);

                        if (!Physics.Raycast(transform.position, dirToPlayer, distToTarget, obstacleMask))
                        {
                            InitChase();
                        }
                    }
                }
            }
        }
        else if (idle)
        {
            Collider[] playerInRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < playerInRadius.Length; i++)
            {
                if (playerInRadius[i].CompareTag("Player"))
                {
                    Vector3 dirToPlayer = (target.position - transform.position).normalized;
                    if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                    {
                        InitPatrol();
                    }
                }
            }
        }
    }

    public int investigateMeterMax;
    int investigateMeter;
    int investigateMeterTimer;

    void CheckSuspicion()
    {
        if(investigateMeter > 0)
        {
            investigateMeterTimer++;
            if(investigateMeterTimer > 100)
            {
                investigateMeter--;
                investigateMeterTimer = 0;
            }

            if(investigateMeter > investigateMeterMax)
            {
                playerLastSeenPos = target.transform.position;
                investigateMeter = 0;
            }
        }
    }

    public void IncreaseSuspicion()
    {
        investigateMeter++;
    }

    public bool stunned = false;
    public GameObject stunParticle;

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
        //if (col.gameObject.CompareTag("Fart"))
        //{
        //if (!stunned)
        //{
        //StartCoroutine(Stun(0.2f));
        //}
        //}

        if (col.gameObject.CompareTag("Player"))
        {
            InitChase();
        }
    }


    IEnumerator Stun(float stunDuration)
    {
        stunned = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsStunned", true);
        enemyAgent.speed = 0;
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
        animator.SetBool("IsStunned", false);
        animator.SetBool("IsWalking", true);
        enemyAgent.speed = enemySpeed;
    }


}
