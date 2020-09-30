 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEditControl : MonoBehaviour
{
	public Button cancelButton;

	public void CancelButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
