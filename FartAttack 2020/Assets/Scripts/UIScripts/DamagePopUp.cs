using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopUp : MonoBehaviour
{
    public Text damagePopUpText;

    Text popUpText;

    public void PopUp(int damage, Transform spawnTransform)
    {
        popUpText = Instantiate(damagePopUpText,transform.position,transform.rotation);
        popUpText.text = damage.ToString();
        popUpText.transform.SetParent(transform);
        popUpText.transform.localScale = new Vector3(1, 1, 1);
        Destroy(popUpText, 0.5f);
    }
}
