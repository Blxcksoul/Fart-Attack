using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFart : MonoBehaviour
{

    public GameObject fart, friesFart, onionFart, eggFart, kimchiFart, bagFart, friesBagFart, onionBagFart, eggBagFart, kimchiBagFart, bakedBeansFart, bakedBeansBagFart;
    public GameObject currentFart;
    public GameObject currentBagFart;
    public GameObject plasticBagTrap;

    public float fartCooldown; //Sets the delay between farts
    public float fartCooldownTimer; //Value that stores the time since the last fart
    public bool effectOn; //True if the player is already under the effects of an item
    public bool bagActive; //True if the player has activated the plastic bag item
    public bool durianEffect;

	Vector3 spawnPos;

    void Start()
    {
        currentFart = fart;
        currentBagFart = bagFart;
        effectOn = false;
        bagActive = false;
        durianEffect = false;
    }

    void Update()
    {
        fartCooldownTimer += Time.deltaTime;

        if(fartCooldownTimer > fartCooldown)
        {
            HandleFart();
        }

    }

    public void HandleFart()//Handles the spawning of the fart effects
    {
        if (bagActive)
        {
            spawnPos = transform.position + new Vector3(0f, 0f, -0.2f);
            plasticBagTrap.GetComponent<PlasticBagScript>().storedFart = currentBagFart;
            Instantiate(plasticBagTrap, spawnPos, Quaternion.identity);
            fartCooldownTimer = 0;
            bagActive = false;
        }
        else
        {
            spawnPos = transform.position + new Vector3(0, 0.1f, 0);
            Instantiate(currentFart, spawnPos, Quaternion.identity);
            GameObject secondfart = Instantiate(currentFart, spawnPos, Quaternion.identity);
            secondfart.GetComponent<Rigidbody>().AddForce(-transform.forward * 20f);
            fartCooldownTimer = 0;

            if (durianEffect)
            {
                secondfart.transform.localScale *= 2;
            }
        }
    }
        

    public void FartChange(int type, float timer)//Changes the type of fart is stored in currentFart and modifies other values based on the effects of the item
    {
        switch (type)
        {
            case 0:
                currentFart = fart;
                currentBagFart = bagFart;
                break;
            case 1:
                currentFart = friesFart;
                currentBagFart = friesBagFart;
                fartCooldown = fartCooldown / 2;
                break;
            case 2:
                currentFart = onionFart;
                currentBagFart = onionBagFart;
                break;
            case 3:
                currentFart = eggFart;
                currentBagFart = eggBagFart;
                break;
            case 4:
                currentFart = kimchiFart;
                currentBagFart = kimchiBagFart;
                break;
            case 5:
                currentFart = bakedBeansFart;
                currentBagFart = bakedBeansBagFart;
                fartCooldown = fartCooldown / 4;
                break;
        }
        effectOn = true;
        StartCoroutine(FartReset(timer));
    }

    IEnumerator FartReset(float timer)//Resets the fart type to the base fart after the item's effects are over
    {
        yield return new WaitForSeconds(timer);
        fartCooldown = 4f;
        currentFart = fart;
        currentBagFart = bagFart;
        effectOn = false;
    }

    public void BeginDurationEffect(float duration)
    {
        StartCoroutine(DurianEffect(duration));
    }

    IEnumerator DurianEffect(float duration)
    {
        durianEffect = true;
        yield return new WaitForSeconds(duration);
        durianEffect = false;
    }
}
