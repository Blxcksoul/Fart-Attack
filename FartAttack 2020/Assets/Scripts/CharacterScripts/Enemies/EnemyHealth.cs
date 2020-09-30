using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public GameObject objectSpawnOnFaint;

    EnemySegmentBarmanager enemySegmentBarmanager;
    public EnemySegmentPointerBarmanager enemySegmentPointerBarmanager;

    public Animator animator;
    public Animator playerAnimator;
    public static bool Win;

    bool fainted = false;
    SceneControl sceneControl;
    private void Start()
    {
        GameObject sceneControlObject = GameObject.Find("SceneControl");
        if (sceneControlObject != null)
        {
            sceneControl = sceneControlObject.GetComponent<SceneControl>();
            sceneControl.Win += LoseAnim;
        }
        
        enemySegmentBarmanager = transform.GetChild(0).GetChild(0).GetComponent<EnemySegmentBarmanager>();
    }

    void LoseAnim()
    {
        if (health > 0)
        {
            animator.SetBool("Lose", true);
        }
        else
        {
            animator.SetBool("Lose", false);
        }
    }

    public float GetHealth()
	{
		return health;
	}

    bool takeDamage;

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Fart":
            case "Fries Fart":
            case "Egg Fart":
            case "Onion Fart":
                DamageHandler(1);
                break;
            case "Kimchi Fart":
                DamageHandler(2);
                break;
        }
    }

    void DamageHandler(float damage, bool durianEffect = false)
    {
        if (!takeDamage)
        {
            if (durianEffect)
            {
                damage *= 0.65f;
            }

            takeDamage = true;
            if (health < damage)
            {
                health -= health;
                enemySegmentBarmanager.UpdateHealth(-health);
                enemySegmentPointerBarmanager.UpdateHealth(-health);
                StartCoroutine(TakeDamage(0.2f));

                if (fainted == false)
                {
                    print("hell");
                    fainted = true;
                    Instantiate(objectSpawnOnFaint, transform.position, transform.rotation);
                }
            }
            else
            {
                health -= damage;
                enemySegmentBarmanager.UpdateHealth(-damage);
                enemySegmentPointerBarmanager.UpdateHealth(-damage);
                StartCoroutine(TakeDamage(0.2f));
            }
        }
    }

    IEnumerator TakeDamage(float damageDuration)
    {
        yield return new WaitForSeconds(damageDuration);
        takeDamage = false;
    }
}
