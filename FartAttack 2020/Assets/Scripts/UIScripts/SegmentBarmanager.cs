using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBarmanager : MonoBehaviour
{
    public Transform healthBar;
    public Transform fartBar;

    public int barHeight;
    public bool fartOn;

    public PlayerHealth playerHealth;
    public PlayerFart playerFart;
    public BoyHealth boyHealth;

    Transform canvTran;
    Transform camTran;
    //Transform 
    RectTransform barHolder;

    Vector3 fB = new Vector3(0f, 1f, 1f);

    GameObject[] barArray;

    int fullVal;

    // Start is called before the first frame update
    void Start()
    {
        canvTran = transform.parent.transform;
        camTran = Camera.main.transform;
        barHolder = transform.GetChild(0).GetComponent<RectTransform>();
        boyHealth = GameObject.Find("BOY").GetComponent<BoyHealth>();
        if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
        {
            fullVal = playerHealth.health;
        }
        else if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
        {
            fullVal = boyHealth.currentHealth;
        }
        barArray = new GameObject[fullVal];
        for (int i = 0; i < fullVal; i++)
        {
            barArray[i] = Instantiate(healthBar.gameObject);
            barArray[i].transform.SetParent(transform.GetChild(0));
            barArray[i].transform.localScale = new Vector3(1, 1, 1);
            barArray[i].transform.localPosition = new Vector3((barHolder.rect.width / fullVal) * i, 0, 0);
            barArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barHolder.rect.width / fullVal);
            barArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeight);
        }
        fartBar.transform.localScale = fB;
        if (!fartOn)
        {
            fartBar.gameObject.SetActive(false);
        }
        
    }

    public void UpdateHealth(int healthUp)
    {
        if (healthUp > 0)
        {
            for (int i = 0; i < healthUp; i++)
            {
                if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
                {
                    barArray[i + playerHealth.health].SetActive(true);
                }
                else if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
                {
                    barArray[i + boyHealth.currentHealth].SetActive(true);
                }
            }
        }
        else
        {
            //Debug.Log("Attempting to remove " + healthUp + " Health Segment(s)");
            for (int i = 0; i > healthUp; i--)
            {
                if (GameObject.Find("BOY").GetComponent<PlayerHealth>() != null)
                {
                    barArray[playerHealth.health - 1 + i].SetActive(false);
                }
                else if (GameObject.Find("BOY").GetComponent<BoyHealth>() != null)
                {
                    barArray[boyHealth.currentHealth - 1 + i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fartOn)
        {
            fB.x = playerFart.fartCooldownTimer / playerFart.fartCooldown;

            fartBar.transform.localScale = fB;
        }
    }

    void LateUpdate()
    {
        canvTran.LookAt(canvTran.position + camTran.rotation * Vector3.forward);
    }
}
