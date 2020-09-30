using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRB;
    public float speed;
    Vector3 direction;
    public Joystick joystick;

    public static bool initialised;
    public static bool walking;
    public static bool gesture;
    bool idle;
    List<SpeedBuff> speedMultipliers = new List<SpeedBuff>();
    float totalSpeedMultiplier = 1;

    Animator animator;

    GameObject enemy;

    void Start()    
    {
        animator = GetComponent<Animator>();
        initialised = false;
        playerRB = GetComponent<Rigidbody>();
        StartCoroutine("Initialise");

        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        if (initialised)
        {
            //Play moving animation
            MovePlayer();
            direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            TurnPlayer();
        }
        if (direction != Vector3.zero && initialised == true)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetTrigger("Gesture");
        }

    }

    public void AddSpeedMultiplier(float amount, float time)
    {
        SpeedBuff newBuff = new SpeedBuff(amount, time);
        speedMultipliers.Add(newBuff);
        UpdateSpeedMultipliers();
        StartCoroutine(SpeedMultiplierDuration(newBuff));
    }

    void UpdateSpeedMultipliers()
    {
        totalSpeedMultiplier = 1;
        foreach (SpeedBuff sb in speedMultipliers)
        {
            totalSpeedMultiplier *= sb.multiplier;
        }
    }

    void MovePlayer()
    {
        playerRB.velocity = direction * speed * totalSpeedMultiplier;
    }

    void TurnPlayer()
    {
        if(direction != Vector3.zero)
            transform.localRotation = Quaternion.LookRotation(direction);
    }

    IEnumerator Initialise()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("walking", true);
        playerRB.velocity = speed * transform.forward;
        yield return new WaitForSeconds(1);
        initialised = true;

        SpeechBubbleManager.instance.PlaySpeechBubble();
    }

    int pushTimer;
    int pushTimerLimit = 10;

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Pushable"))
        {
            pushTimer++;

            if(pushTimer > pushTimerLimit)
            {
                enemy.GetComponent<EnemyMovement>().IncreaseSuspicion();
                pushTimer = 0;
            }
        }
    }

    IEnumerator SpeedMultiplierDuration(SpeedBuff _ref)
    {
        yield return new WaitForSeconds(_ref.seconds);
        speedMultipliers.Remove(_ref);
        UpdateSpeedMultipliers();
    }
}

//struct can be kept as reference, however the data it stores is pure data, no reference.
struct SpeedBuff
{
    public float multiplier;
    public float seconds;

    public SpeedBuff(float _multiplier, float _seconds)
    {
        multiplier = _multiplier;
        seconds = _seconds;
    }
}