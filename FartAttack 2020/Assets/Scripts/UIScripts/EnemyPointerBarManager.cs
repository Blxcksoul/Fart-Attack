using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointerBarManager : MonoBehaviour
{
    //public Transform healthBar;
    public RectTransform healthBar;
    RectTransform objectRect;

    public EnemyHealth enemyHealth;

    Vector3 hB = new Vector3(1f, 0.3f, 1f);

    float fullVal;

    void Start()
    {
        fullVal = enemyHealth.health;
        objectRect = transform.GetComponentInParent<RectTransform>();
    }

    void Update()
    {
        hB.x = 1.4f * (enemyHealth.health / fullVal);

        healthBar.sizeDelta = hB;
    }

    void LateUpdate()
    {

        objectRect.rotation = Quaternion.Euler(objectRect.rotation.x, objectRect.rotation.y, objectRect.rotation.z);
    }
}
