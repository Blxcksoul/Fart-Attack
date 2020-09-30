using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    int damageCooldown;

    Renderer[] playerRenderer;
    Material headDefault;
    Material bodyDefault;
    Material hurtMaterial;

    public int enemyDamage;

    public DamagePopUp damagePopUp;
    public Transform damagePopUpPosition;

    bool IsAttackingBool = false;

    SegmentBarmanager segmentBarmanager;

    void Start()
    {
        playerRenderer = GetComponentsInChildren<Renderer>();

        hurtMaterial = Resources.Load("Materials/HurtMaterial", typeof(Material)) as Material;

        headDefault = playerRenderer[0].material;
        bodyDefault = playerRenderer[1].material;

        segmentBarmanager = transform.GetChild(2).GetChild(0).GetComponent<SegmentBarmanager>();

        maxHealth = health;
    }

	void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (!col.gameObject.GetComponent<EnemyMovement>().stunned)
            {
                TakeDamage(col);
            }
        }
    }

    void TakeDamage(Collider col)
    {
        if (health <= 0)
        {
            //Game over
        }


        damageCooldown++;
        if (damageCooldown > 100 && health >= 1)
        {
            //flashing and number indication
            StartCoroutine(DamageAnim(col));
            damagePopUp.PopUp(enemyDamage, damagePopUpPosition);
            segmentBarmanager.UpdateHealth(0 - enemyDamage);
            health -= enemyDamage;
            damageCooldown = 0;
        }
    }

    IEnumerator DamageAnim(Collider col)
    {
        Animator grannyAnimator = col.GetComponentInChildren<Animator>();
        IsAttackingBool = true;
        playerRenderer[0].material = hurtMaterial;
        playerRenderer[1].material = hurtMaterial;
        grannyAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.3f);
        playerRenderer[0].material = headDefault;
        playerRenderer[1].material = bodyDefault;
        IsAttackingBool = false;
        grannyAnimator.SetBool("IsAttacking", false);
    }

	public int GetHealth()
	{
		return health;
	}
}
