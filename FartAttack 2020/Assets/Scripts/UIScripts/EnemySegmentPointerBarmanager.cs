using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySegmentPointerBarmanager : MonoBehaviour
{
    public Transform healthBar;
    RectTransform objectRect;


    public int barHeight;

    public EnemyHealth enemyHealth;

    Transform canvTran;
    Transform camTran;
    RectTransform barHolder;


    GameObject[] barArray;

    int fullVal;
    
    void Start()
    {
        fullVal = Mathf.RoundToInt(enemyHealth.health);
        objectRect = transform.GetComponentInParent<RectTransform>();

        barHolder = transform.GetChild(0).GetComponent<RectTransform>();
        fullVal = Mathf.RoundToInt(enemyHealth.health);
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
        objectRect.rotation = Quaternion.Euler(objectRect.rotation.x, objectRect.rotation.y, objectRect.rotation.z);
    }
}
