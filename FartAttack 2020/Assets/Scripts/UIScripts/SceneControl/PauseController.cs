using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public GameObject joystick;

    public GameObject pauseOverlay;
    public GameObject pauseButton;
    public GameObject pausedIndicator;

    public GameObject playButton;
    public GameObject homeButton;
    public GameObject restartButton;

    public GameObject musicButton;
    public GameObject musicCross;

    GameObject[] enemyPointers;
    bool muted;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.SetActive(true);
        UIEnemyPointer[] uiep = FindObjectsOfType<UIEnemyPointer>();

        enemyPointers = new GameObject[uiep.Length];

        for (int i = 0; i < uiep.Length; i++)
        {
            enemyPointers[i] = uiep[i].gameObject;
        }
    }

    public void SetEnemyPointersActive(bool on)
    {
        foreach ( GameObject o in enemyPointers)
        {
            o.SetActive(on);
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        joystick.SetActive(false);
        pauseButton.SetActive(false);
        SetEnemyPointersActive(false);
        pauseOverlay.SetActive(true);
        playButton.SetActive(true);
        homeButton.SetActive(true);
        musicButton.SetActive(true);
	}

    public void PlayButton()
    {
        Time.timeScale = 1;
        pauseOverlay.SetActive(false);
        playButton.SetActive(false);
        homeButton.SetActive(false);
        musicButton.SetActive(false);
        pausedIndicator.SetActive(false);
        SetEnemyPointersActive(true);

        joystick.SetActive(true);
        pauseButton.SetActive(true);
    }

    public void MusicButton()
    {
        if (muted)
        {
            muted = false;
            musicCross.SetActive(muted);
        }
        else
        {
            muted = true;
            musicCross.SetActive(muted);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadSceneAsync(ChapterSelectManager.selectedChapter);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
