using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarManager : MonoBehaviour
{
    public Transform healthBar;

    public EnemyHealth enemyHealth;

    public Transform canvTran;
    public Transform camTran;

    Vector3 hB = new Vector3(1f, 1f, 1f);

    float fullVal;
    // Start is called before the first frame update
    void Start()
    {
        fullVal = enemyHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        hB.x = enemyHealth.health / fullVal;

        healthBar.transform.localScale = hB;
    }

    void LateUpdate()
    {
        canvTran.LookAt(canvTran.position + camTran.rotation * Vector3.forward);
    }
}
