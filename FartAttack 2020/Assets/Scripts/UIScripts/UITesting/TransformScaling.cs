using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransformScaling : MonoBehaviour
{
	bool expanded1;
	bool expanded2;
	bool expanded3;

	public Button button1;
	public Button button2;
	public Button button3;

	Button currentExpanded; 
    public static int currentExpandedIndex;

	public Button editButton1, starButton1;
	public Button editButton2, starButton2;
	public Button editButton3, starButton3;

	public void Start()
	{
        ButtonScaleOnStart(currentExpandedIndex);
		Button1Scaling();
	}

    void ButtonScaleOnStart(int index)
    {
        if(currentExpandedIndex == 1)
        {
			Button1Scaling();
        }

        if (currentExpandedIndex == 2)
        {
            Button2Scaling();
        }

        if (currentExpandedIndex == 3)
        {
            Button3Scaling();
        }
    }

	public void Button1Scaling()
	{
		currentExpanded = button1;

		Debug.Log("1");
		currentExpanded.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 1200f);
		currentExpanded.transform.localPosition = new Vector3(0, 511, 0);

		button2.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button2.transform.localPosition = new Vector3(0, -375, 0);

		button3.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button3.transform.localPosition = new Vector3(0, -962, 0);

		editButton1.gameObject.SetActive(true);
		starButton1.gameObject.SetActive(true);

		editButton2.gameObject.SetActive(false);
		starButton2.gameObject.SetActive(false);

		editButton3.gameObject.SetActive(false);
		starButton3.gameObject.SetActive(false);
	}

	public void Button2Scaling()
	{
		currentExpanded = button2;

        Debug.Log("2");
		currentExpanded.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 1200f);
		currentExpanded.transform.localPosition = new Vector3(0, -88, 0);

		button1.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button1.transform.localPosition = new Vector3(0, 782, 0);

		button3.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button3.transform.localPosition = new Vector3(0, -962, 0);

		editButton1.gameObject.SetActive(false);
		starButton1.gameObject.SetActive(false);

		editButton2.gameObject.SetActive(true);
		starButton2.gameObject.SetActive(true);

		editButton3.gameObject.SetActive(false);
		starButton3.gameObject.SetActive(false);

	}

	public void Button3Scaling()
	{
		currentExpanded = button3;

        Debug.Log("3");
		currentExpanded.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 1200f);
		currentExpanded.transform.localPosition = new Vector3(0, -676, 0);

		button1.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button1.transform.localPosition = new Vector3(0, 780, 0);

		button2.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 600f);
		button2.transform.localPosition = new Vector3(0, 194, 0);

		editButton1.gameObject.SetActive(false);
		starButton1.gameObject.SetActive(false);

		editButton2.gameObject.SetActive(false);
		starButton2.gameObject.SetActive(false);

		editButton3.gameObject.SetActive(true);
		starButton3.gameObject.SetActive(true);

	}

	public void EditButton(int slotIndex)
	{
		SceneManager.LoadScene("LevelEdit");
        BuildEditData.slotIndex = slotIndex;
	}

    public void StarButton(int index)
    {
        currentExpandedIndex = index;
    }

	public void CancelButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}