using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public delegate void LevelEvent();
    public static event LevelEvent NextStage;

    public Text commentText;
    public Text levelText;
    public Text timeTakenText;
    public GameObject banner;

    public GameObject timerSymbol;
    public GameObject faintSymbol;
    public GameObject bearSymbol;

    public GameObject timerCover;
    public GameObject faintCover;
    public GameObject teddyCover;

    public Button nextLevelButton;
    public Button homeButton;
    public Button retryButton;
    public GameObject winInfo;

    public GameObject winOnlyObjects;
    public GameObject loseOnlyObjects;

    //bool camVecNotDone = false;
    int winCheck = 0;

    float camTimer = 0f;

    bool showSkippableAd = false;
    bool showForceAd = false;
    // Start is called before the first frame update
    void Start()
    {
        //originPos = Vector3.zero;
        if (ChapterSelectManager.currentLevel == 2 || ChapterSelectManager.currentLevel == 7)
        {
            showSkippableAd = true;
        }

        if (ChapterSelectManager.currentLevel == 5)
        {
            showForceAd = true;
        }

        if (!NextStageExists())
        {
            showForceAd = true;

        }
    }

    // Update to move the object slowly accross multiple frames into place
    void LateUpdate()
    {
        Camera mainCam = Camera.main;
        Vector3 neededVec;

        //if (camVecNotDone)
        //{
            
        //    mainCam.transform.parent.GetComponent<CameraController>().gameNotEnd = false;
        //    camVecNotDone = false;
        //}
        if (winCheck == 1)
        {
            if (camTimer < 2.1f)
            {
                camTimer += Time.deltaTime;

                Vector3 originPos = mainCam.transform.position;
                Vector3 toAdd = new Vector3(0f, 1f, 0f);
                Vector3 faceOfEnemy = transform.parent.GetComponent<SceneControl>().enemyObject.transform.TransformPoint(transform.forward * 4.5f) + toAdd;
                neededVec = Vector3.zero;

                Quaternion originalRot = mainCam.transform.rotation;
                Quaternion rotOfEnemy = Quaternion.LookRotation(transform.parent.GetComponent<SceneControl>().enemyObject.transform.position - faceOfEnemy);

                
                mainCam.transform.position = Vector3.SmoothDamp(originPos, faceOfEnemy, ref neededVec, 0.08f);
                mainCam.transform.rotation = Quaternion.Slerp(originalRot, rotOfEnemy, 0.1f);
            }
            if (camTimer >= 2.1f)
            {
                mainCam.transform.RotateAround(transform.parent.GetComponent<SceneControl>().enemyObject.transform.position, Vector3.up, Time.deltaTime * 10);
            }
            
        }
        else if(winCheck == 2)
        {
            if (camTimer < 6.0f)
            {
                camTimer += Time.deltaTime;

                Vector3 originPos = mainCam.transform.position;
                Vector3 toAdd = new Vector3(0f, 12f, 0f);
                Vector3 abovePlayer = transform.parent.GetComponent<SceneControl>().playerObject.transform.position + toAdd;
                neededVec = Vector3.zero;

                Quaternion originalRot = mainCam.transform.rotation;
                Quaternion lookDown = Quaternion.LookRotation(Vector3.down, Vector3.forward);

                mainCam.transform.position = Vector3.SmoothDamp(originPos, abovePlayer, ref neededVec, 0.2f);
                mainCam.transform.rotation = Quaternion.Slerp(originalRot, lookDown, 0.01f);
            }
        }
        

        

    }

    public void OpenEndScreen(bool didWin)
    {
        commentText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);

        //Only used for editor testing, this will never be needed in actual gameplay
        if (ChapterSelectManager.currentChapterSelected != null)
        {
            levelText.text = "Chapter " + ChapterSelectManager.currentChapterSelected.chapter + " Level " + ChapterSelectManager.currentLevel;
        }
        //camVecNotDone = true;
        if (didWin)
        {
            commentText.text = "You Won, nice!";
            winOnlyObjects.SetActive(true);
            winCheck = 1;

            if (!NextStageExists())
            {
                if (ChapterSelectManager.currentChapterSelected != null)
                {
                    commentText.text = "Completed Chapter " + ChapterSelectManager.currentChapterSelected.chapter + "!";
                }
            }
        }
        else
        {
            loseOnlyObjects.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);
            commentText.text = "Try again, you got this!";
            winCheck = 2;
        }

        winInfo.SetActive(true);

    }

    public void handleGreyIcons(bool teddy, bool faint, bool time)
    {
        if (faint)
        {
            faintCover.SetActive(false);
        }
        if (time)
        {
            timerCover.SetActive(false);
        }

        if (teddy)
        {
            teddyCover.SetActive(false);
        }
    }

    void TriggerNextStageEvent()
    {
        NextStage?.Invoke();
    }

    public void NextLevelButton()
    {
        if (showSkippableAd)
        {
            UnityAd.ShowSkippableAd();
            showSkippableAd = false;
            return;
        }

        if (showForceAd)
        {
            UnityAd.ShowForceAd();
            showForceAd = false;
            return;
        }

        if (SceneManager.GetActiveScene().name == "Multiplayer")
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        //Not only checks, but also increases the current sub chapter, it is a unique chunk of codes.
        ChapterSelectManager.currentLevel += 1;
        string nextSubLevelName = ChapterSelectManager.selectedChapter + "_" + ChapterSelectManager.currentLevel.ToString();
        bool nextExists = Application.CanStreamedLevelBeLoaded(nextSubLevelName);
        if (!nextExists)
        {
            SceneManager.LoadScene("Level Select");
            if (ChapterSelectManager.currentChapterSelected == null)
            {
                Debug.Log("Game does not work as intended if not played through LevelSelection scene at least");
            }
            else
            {
                PlayerPrefs.SetInt(ChapterSelectManager.saveDataName, ChapterSelectManager.currentChapterSelected.chapter);
                PlayerPrefs.Save();
            }
        }
        else
        {
            SceneManager.LoadScene(nextSubLevelName);
        }

    }

    bool NextStageExists()
    {
        int nextStageNum = ChapterSelectManager.currentLevel + 1;
        string nextSubLevelName = ChapterSelectManager.selectedChapter + "_" + nextStageNum.ToString();
        bool nextExists = Application.CanStreamedLevelBeLoaded(nextSubLevelName);
        return nextExists;
    }

    public void RetryLevelButton()
    {
        if (SceneManager.GetActiveScene().name == "Multiplayer")
        {
            SceneManager.LoadScene("Multiplayer");
        }
        else
        {
            ChapterSelectManager.EnterSelectedChapterP();
        }
    }

    public void HomeButton()
    {
        if (SceneManager.GetActiveScene().name == "Multiplayer")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Level Select");
        }
        //Debug.Log("A reminder to update the scene to the actual menu in future!");
    }
}