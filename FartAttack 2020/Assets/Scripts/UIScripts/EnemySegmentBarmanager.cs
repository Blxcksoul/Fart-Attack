using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySegmentBarmanager : MonoBehaviour
{
    public Transform healthBar;

    public int barHeight;

    EnemyHealth enemyHealth;

    Transform canvTran;
    Transform camTran;
    RectTransform barHolder;


    GameObject[] barArray;

    int fullVal;
    
    void Start()
    {
        canvTran = transform.parent.transform;
        camTran = Camera.main.transform;
        enemyHealth = transform.parent.parent.GetComponent<EnemyHealth>();
        barHolder = transform.GetChild(0).GetComponent<RectTransform>();
        fullVal = Mathf.RoundToInt(enemyHealth.health);
        barArray = new GameObject[fullVal];
        for (int i = 0; i < fullVal; i++)
        {
            barArray[i] = Instantiate(healthBar.gameObject);
            barArray[i].transform.SetParent(transform.GetChild(0));
            barArray[i].transform.localScale = new Vector3(1, 1, 1);
            barArray[i].transform.localPosition = new Vector3((barHolder.rect.width / fullVal) * i, 0, 0);
            barArray[i].transform.localRotation = Quaternion.identity;
            barArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barHolder.rect.width / fullVal);
            barArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeight);
        }
    }

    public void UpdateHealth(float healthUp)
    {
        if (healthUp > 0)
        {
            for (int i = 0; i < healthUp; i++)
            {
                barArray[i + Mathf.RoundToInt(enemyHealth.health)].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i > healthUp; i--)
            {
                barArray[Mathf.RoundToInt(enemyHealth.health) - i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        canvTran.LookAt(canvTran.position + camTran.rotation * Vector3.forward);
    }
}
