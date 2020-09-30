using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSelect : MonoBehaviour
{
	//Declare RectTransform in script
	RectTransform currentSelect;

	//Reference value used for the Smoothdamp method
	private Vector3 buttonVelocity = Vector3.zero;

	//Smooth time
	private float smoothTime = 0.2f;

	//The new position of your button
	Vector3 newPos = new Vector3(0, 0, 0);

	void Start()
	{
		//Get the RectTransform component
		currentSelect = GetComponent<RectTransform>();
	}

	void Update()
	{	
		//Update the localPosition towards the newPos
		currentSelect.localPosition = Vector3.SmoothDamp(currentSelect.localPosition, newPos, ref buttonVelocity, smoothTime);
	}

	public void ChangePos(Vector3 position)
	{
		newPos = position;
	}
}
