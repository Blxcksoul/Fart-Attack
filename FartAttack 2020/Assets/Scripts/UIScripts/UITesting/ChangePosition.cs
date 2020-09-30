using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePosition : MonoBehaviour
{
	public GameObject currentSelect;
	//CurrentSelect something;

    // Start is called before the first frame update
    void Start()
    {
		//something = currentselect.GetComponent<CurrentSelect>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ChangeToThisPosition()
	{
		currentSelect.GetComponent<CurrentSelect>().ChangePos(transform.localPosition);
	}
}
