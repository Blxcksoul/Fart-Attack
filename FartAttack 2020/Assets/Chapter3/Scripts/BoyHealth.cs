using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    int damageCooldown;
    public int attackDamage;

    Renderer[] playerRenderer;
    Material headDefault;
    Material bodyDefault;
    Material hurtMaterial;

    public DamagePopUp damagePopUp;
    public Transform damagePopUpPosition;

    SegmentBarmanager segmentBarmanager;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponentsInChildren<Renderer>();

        hurtMaterial = Resources.Load("Materials/HurtMaterial", typeof(Material)) as Material;

        headDefault = playerRenderer[0].material;
        bodyDefault = playerRenderer[1].material;

        segmentBarmanager = transform.GetChild(2).GetChild(0).GetComponent<SegmentBarmanager>();

        maxHealth = 4;
        currentHealth = 4;
        damageCooldown = 0;
    }

    //TakeDamage method which takes in an interger which is the amount of damage the enemy deals
    public void TakeDamage(int attackDamage)
    {
        damageCooldown++;
        if (damageCooldown >= 1 && currentHealth >= 1)
        {
            //Debug.Log("TakeDamage Called");
            //flashing and number indication
            damagePopUp.PopUp(attackDamage, damagePopUpPosition);
            segmentBarmanager.UpdateHealth(0 - attackDamage);
            currentHealth -= attackDamage;
            damageCooldown = 0;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
