using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigmanager : MonoBehaviour
{
    public void RunVigAnim()
    {
        GetComponent<Animator>().Play("Vig_hurt");
    }
}
