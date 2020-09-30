using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEdit : MonoBehaviour
{
	public GameObject FurnitureScrollBar;
	public GameObject PushablesScrollBar;
	public GameObject DecorationsScrollBar;
	public GameObject DefendersScrollBar;

    public Image FurnitureScrollBars;
	public GameObject ScrollBars;
	public GameObject DropdownBar;
    float scrollBarHeight;

	public GameObject UIButtons;

	RectTransform dropdownBar;
	RectTransform uiButtons;

	public Button cancelButton;

    bool down;

	// Start is called before the first frame update
	void Start()
    {
		dropdownBar = DropdownBar.GetComponent<RectTransform>();
		uiButtons = UIButtons.GetComponent<RectTransform>();

        down = false;
        scrollBarHeight = FurnitureScrollBars.rectTransform.sizeDelta.y;
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveUI()
    {
        if (down)
        {
            dropdownBar.Translate(new Vector3(0,scrollBarHeight,0));
            uiButtons.Translate(new Vector3(0, scrollBarHeight, 0));
            ScrollBars.gameObject.SetActive(true);
        }
        else
        {
            dropdownBar.Translate(new Vector3(0, -scrollBarHeight, 0));
            uiButtons.Translate(new Vector3(0, -scrollBarHeight, 0));
            ScrollBars.gameObject.SetActive(false);
        }

        down = !down;
    }

	public void FurnitureSelect()
	{
			FurnitureScrollBar.gameObject.SetActive(true);

			PushablesScrollBar.gameObject.SetActive(false);
			DecorationsScrollBar.gameObject.SetActive(false);
			DefendersScrollBar.gameObject.SetActive(false);
	}

	public void PushablesSelect()
	{
			PushablesScrollBar.gameObject.SetActive(true);

			FurnitureScrollBar.gameObject.SetActive(false);
			DecorationsScrollBar.gameObject.SetActive(false);
			DefendersScrollBar.gameObject.SetActive(false);
	}

	public void DecorationsSelect()
	{
		DecorationsScrollBar.gameObject.SetActive(true);

		FurnitureScrollBar.gameObject.SetActive(false);
		PushablesScrollBar.gameObject.SetActive(false);
		DefendersScrollBar.gameObject.SetActive(false);
	}

	public void DefendersSelect()
	{
		DefendersScrollBar.gameObject.SetActive(true);

		FurnitureScrollBar.gameObject.SetActive(false);
		PushablesScrollBar.gameObject.SetActive(false);
		DecorationsScrollBar.gameObject.SetActive(false);
	}

	public void CancelButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
