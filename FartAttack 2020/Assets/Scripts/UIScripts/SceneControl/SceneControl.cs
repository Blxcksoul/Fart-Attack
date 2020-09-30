using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
	public GameObject joystick;
    public GameObject invHolder;
	public GameObject pauseButton;
    public GameObject timer;

	public InventoryScript inventoryScript;

    public Transform enemyPointerParent;
    public GameObject enemyPointerPrefab;
	public string enemyHealthBarName;
    public GameObject doorPointer;

	public PlayerHealth playerHealth;
	public EnemyHealth enemyHealth;
    public BoyHealth boyHealth;

	public GameObject playerObject;
	public GameObject enemyObject;
    public Transform NextLevelTriggerTransform;

    public GameObject Sky;

	public Animator playerAnimator;
	public Animator grannyAnimator;
    public Animator doorAnimator;

    public Text bearAmountText;

    PauseController pauseController;
    EndScreenController endScreenController;

    public int bearsPickedup;
    public int bearRequirement;

    bool madeFaint = false;
    bool madeTime = false;
    bool madeTeddy = false;

    bool playerDownNotDone = true;
    bool[] enemiesDownNotDone;

    public float maxTime;

    GameObject[] enemiesInLevel;
    EnemyHealth[] enemiesHealth;
    Animator[] enemiesAnimator;
    GameObject[] enemiesHealthBar;
    GameObject[] enemiesPointer;

    public delegate void GameEnd();
    public event GameEnd Win;


    void Awake()
	{
        endScreenController = transform.GetChild(0).GetComponent<EndScreenController>();
        pauseController = transform.GetChild(1).GetComponent<PauseController>();

		Time.timeScale = 1;
		joystick.gameObject.SetActive(true);
		//pauseButton.gameObject.SetActive(true);

        if (Sky)
        {
            Sky.SetActive(true);
        }

        bearAmountText.text = "";
        GameObject[] totalBearsInLevel = GameObject.FindGameObjectsWithTag("Teddy Bear");
        bearRequirement = totalBearsInLevel.Length;
        //muted = false;

        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesHealth = new EnemyHealth[enemiesInLevel.Length];
        enemiesDownNotDone = new bool[enemiesInLevel.Length];
        enemiesAnimator = new Animator[enemiesInLevel.Length];
        enemiesHealthBar = new GameObject[enemiesInLevel.Length];
        enemiesPointer = new GameObject[enemiesInLevel.Length];

        for (int i = 0; i < enemiesInLevel.Length; i++)
        {
            enemiesHealth[i] = enemiesInLevel[i].GetComponent<EnemyHealth>();
            enemiesDownNotDone[i] = true;
            enemiesAnimator[i] = enemiesInLevel[i].GetComponentInChildren<Animator>();
            enemiesHealthBar[i] = enemiesInLevel[i].transform.Find("EnemyCanvas").Find(enemyHealthBarName).gameObject;
            enemiesPointer[i] = Instantiate(enemyPointerPrefab, enemyPointerParent);
            UIEnemyPointer ep = enemiesPointer[i].GetComponent<UIEnemyPointer>();
            ep.enemyTrans = enemiesInLevel[i].transform;
            ep.enemyHealthBarTrans = enemiesHealthBar[i].transform;

            Transform segmentHealthBar = enemiesPointer[i].transform.GetChild(0).GetChild(0);

            //SegmentBarmanager sb = segmentHealthBar.GetComponent<SegmentBarmanager>();
            //sb.playerHealth = playerHealth;
            //sb.playerFart = playerHealth.gameObject.GetComponent<PlayerFart>();

            EnemySegmentPointerBarmanager espb = segmentHealthBar.GetComponent<EnemySegmentPointerBarmanager>();
            espb.enemyHealth = enemiesHealth[i];
            //print("wadafreak");

            enemiesHealth[i].enemySegmentPointerBarmanager = espb;
        }
    }

	void Update()
	{
        if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
        {
            if (boyHealth.GetHealth() <= 0 && playerDownNotDone)
            {
                //Debug.Log("called");
                timer.transform.parent.gameObject.SetActive(false);

                GameLose();

                inventoryScript.gameOver = true;

                PlayerSeizeMovement();

                playerAnimator.SetBool("PlayerFaint", true);

                foreach (Animator a in enemiesAnimator)
                {
                    a.SetBool("GrannyWin", true);
                }

                playerDownNotDone = false;
            }
        }
        else if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
        {
            if (playerHealth.GetHealth() <= 0 && playerDownNotDone)
            {
                //Debug.Log("called");
                timer.transform.parent.gameObject.SetActive(false);

                GameLose();

                inventoryScript.gameOver = true;

                PlayerSeizeMovement();

                playerAnimator.SetBool("PlayerFaint", true);

                foreach (Animator a in enemiesAnimator)
                {
                    a.SetBool("GrannyWin", true);
                }

                playerDownNotDone = false;
            }
        }

        for (int i = 0; i < enemiesInLevel.Length; i++)
        {
            if (enemiesHealth[i].GetHealth() <= 0 && enemiesDownNotDone[i])
            {
                EnemySeizeMovement(enemiesInLevel[i]);

                enemiesAnimator[i].SetBool("IsFainted", true);
                enemiesHealthBar[i].gameObject.SetActive(false);
                enemiesPointer[i].gameObject.SetActive(false);

                enemiesDownNotDone[i] = false;

                if (GameObject.Find("Barrier") != null)
                {
                    Destroy(GameObject.Find("Barrier"));
                }
            }
        }

        CheckAllFaints();


        if (bearsPickedup >= bearRequirement)
        {
            madeTeddy = true;
        }
    }

    void CheckAllFaints()
    {
        for (int i = 0; i < enemiesDownNotDone.Length; i++)
        {
            if (enemiesDownNotDone[i] == true)
            {
                madeFaint = false;
                return;
            }
        }

        madeFaint = true;
    }

	private void EnemySeizeMovement(GameObject enemyObject)
	{
        if (GameObject.Find("Enemy").GetComponent<EnemyMovement>() != null)
        {
            enemyObject.GetComponent<EnemyMovement>().enabled = false;
        }
        if (GameObject.Find("Enemy").GetComponent<SportsTeacherController>() != null)
        {
            enemyObject.GetComponent<SportsTeacherController>().enabled = false;
        }
        if (GameObject.Find("Enemy").GetComponent<PrincipalController>() != null)
        {
            enemyObject.GetComponent<PrincipalController>().enabled = false;
        }
        enemyObject.GetComponent<SphereCollider>().enabled = false;
		enemyObject.GetComponent<BoxCollider>().enabled = false;
		enemyObject.GetComponent<NavMeshAgent>().enabled = false;
	}

	private void PlayerSeizeMovement()
	{
		joystick.gameObject.SetActive(false);

		playerObject.GetComponent<PlayerMovement>().enabled = false;
		playerObject.GetComponent<PlayerFart>().enabled = false;
        if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
        {
            playerObject.GetComponent<PlayerHealth>().enabled = false;
        }
        if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
        {
            playerObject.GetComponent<BoyHealth>().enabled = false;
        }
        playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (GameObject.Find("Enemy").GetComponent<EnemyMovement>() != null)
        {
            enemyObject.GetComponent<EnemyMovement>().enabled = false;
        }
        if (GameObject.Find("Enemy").GetComponent<SportsTeacherController>() != null)
        {
            enemyObject.GetComponent<SportsTeacherController>().enabled = false;
        }
        if (GameObject.Find("Enemy").GetComponent<PrincipalController>() != null)
        {
            enemyObject.GetComponent<PrincipalController>().enabled = false;
        }
        enemyObject.GetComponent<BoxCollider>().enabled = false;
		enemyObject.GetComponent<NavMeshAgent>().enabled = false;
	}

	private void GameLose()
	{
        endScreenController.OpenEndScreen(false);
        endScreenController.handleGreyIcons(madeTeddy, madeFaint, madeTime);
        joystick.SetActive(false);
        pauseButton.SetActive(false);
        foreach (GameObject ep in enemiesPointer)
        {
            ep.SetActive(false);
        }
        invHolder.SetActive(false);
        doorPointer.SetActive(false);
        bearsPickedup = 0;
        float timeTaken = timer.GetComponent<EngagedTimerController>().truetime;
        int minute = (int)(timeTaken / 60f);
        int seconds = (int)(timeTaken % 60f);
        endScreenController.timeTakenText.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }

    private void GameWin()
    {
        endScreenController.OpenEndScreen(true);
        endScreenController.handleGreyIcons(madeTeddy, madeFaint, madeTime);
        joystick.SetActive(false);
        pauseButton.SetActive(false);

        foreach(GameObject ep in enemiesPointer)
        {
            ep.SetActive(false);
        }
        invHolder.SetActive(false);
        doorPointer.SetActive(false);
        bearsPickedup = 0;
        float timeTaken = timer.GetComponent<EngagedTimerController>().truetime;
        int minute = (int)(timeTaken / 60f);
        int seconds = (int)(timeTaken % 60f);
        endScreenController.timeTakenText.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }

    public void BearPickup()
    {
        bearsPickedup += 1;

        bearAmountText.text = "x" + bearsPickedup.ToString();

        if (bearsPickedup >= bearRequirement)
        {
            doorAnimator.SetBool("NextLevel", true);
            doorPointer.gameObject.SetActive(true);
        }
    }

    public void BearWin()
    {
        if (maxTime > timer.GetComponent<EngagedTimerController>().truetime)
        {
            madeTime = true;
            
        }
        timer.transform.parent.gameObject.SetActive(false);

        GameWin();

        inventoryScript.gameOver = true;

        PlayerSeizeMovement();

        playerAnimator.SetBool("Win", true);


        Win?.Invoke();

        for (int i = 0; i < enemiesInLevel.Length; i++)
        {
            EnemySeizeMovement(enemiesInLevel[i]);
        }
        //Granny animator will now play Lose animation by using delegate event assignment to be called.
    }
}
