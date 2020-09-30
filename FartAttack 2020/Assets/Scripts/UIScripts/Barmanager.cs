using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barmanager : MonoBehaviour
{
    public Transform healthBar;
    public Transform fartBar;

    public PlayerHealth playerHealth;
    public PlayerFart playerFart;

    public Transform canvTran;
    public Transform camTran;

    Vector3 hB = new Vector3(1f, 1f, 1f);
    Vector3 fB = new Vector3(0f, 1f, 1f);

    float fullVal;
    // Start is called before the first frame update
    void Start()
    {
        fullVal = playerHealth.health;
        fartBar.transform.localScale = fB;
    }

    // Update is called once per frame
    void Update()
    {
        hB.x = playerHealth.health / fullVal;
        fB.x = playerFart.fartCooldownTimer / playerFart.fartCooldown;

        healthBar.transform.localScale = hB;
        fartBar.transform.localScale = fB;
    }

    void LateUpdate()
    {
        canvTran.LookAt(canvTran.position + camTran.rotation * Vector3.forward);
    }
}
