using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class ChapterSelectManager : MonoBehaviour
{
    public ScrollRect scrollRect;

    public Transform chapterSelectContainer;
    RectTransform chapterSelectContainerRect;
    public Transform chapterLockedPrefab;


    public static ChapterPreview currentChapterSelected;

    public float belowThisVelocityToStartLerp;
    public float snapDistance;
    public float lerpPercent;

    [HideInInspector]
    public int editorChapterNumber;
    [HideInInspector]
    public GameObject editorChapterObject;
    [HideInInspector]
    public List<ChapterPreview> chapterPreviews = new List<ChapterPreview>();

    List<ChapterPreview> spawnedChapters = new List<ChapterPreview>();
    float firstLevelXPos, secondLevelXPos, xPosDiff, xPosDiffHalf;
    List<float> levelsXPos = new List<float>();
    public Coroutine slideToSelected;

    public bool dragging = false;

    public static string selectedChapter;
    public static int currentLevel;
    public static string saveDataName = "CompletedChapter";

    static int completedChapter = 0;

    public int spacingBetweenChapters;

    private void Awake()
    {
        //ResetSave();
        completedChapter = PlayerPrefs.GetInt(saveDataName);
        chapterSelectContainerRect = chapterSelectContainer.GetComponent<RectTransform>();
    }

    private void Start()
    {
        SpawnAllLevels();
        SelectChapterLerp(1);
    }

    private void Update()
    {
        if (!dragging)
        {
            if (scrollRect.velocity.magnitude < belowThisVelocityToStartLerp)
            {
                if (slideToSelected == null)
                {
                    SelectClosestLevel();
                }
            }
        }
    }

    IEnumerator SlideToSelected(Vector2 targetPos)
    {
        scrollRect.velocity = new Vector2(0, 0);

        while (Vector2.Distance(chapterSelectContainerRect.anchoredPosition, targetPos) > snapDistance)
        {
            chapterSelectContainerRect.anchoredPosition = 
                Vector2.Lerp(chapterSelectContainerRect.anchoredPosition, targetPos, lerpPercent);
            yield return new WaitForEndOfFrame();
        }
        chapterSelectContainerRect.anchoredPosition = targetPos;
        slideToSelected = null;
    }

    public void CheckSlideToSelectedCoroutine()
    {
        if (slideToSelected != null)
        {
            StopCoroutine(slideToSelected);
            slideToSelected = null;
        }
    }

    //Enter selected chapter programitically, static cannot be pressed in button events, so lmao
    public static void EnterSelectedChapterP()
    {
        if (completedChapter + 1 < currentChapterSelected.chapter)
        {
            Debug.Log("You have not completed previous chapter to enter this chapter");
            return;
        }

        currentLevel = 1;
        selectedChapter = "chapter" + currentChapterSelected.chapter.ToString();
        SceneManager.LoadScene(selectedChapter);

        InventoryScript.inventory = new string[] { "", "", "", "" };
    }

    //For button use.
    public void EnterSelectedChapter()
    {
        if (completedChapter + 1 < currentChapterSelected.chapter)
        {
            Debug.Log("You have not completed previous chapter to enter this chapter");
            return;
        }

        currentLevel = 1;
        selectedChapter = "chapter" + currentChapterSelected.chapter.ToString();
        SceneManager.LoadScene(selectedChapter);

        InventoryScript.inventory = new string[] { "", "", "", "" };
    }

    public void SelectChapterLerp(int chapter)
    {
        float equation = -xPosDiff * (chapter - 1);

        CheckSlideToSelectedCoroutine();
        slideToSelected = StartCoroutine(SlideToSelected(new Vector2(equation, 0)));
    }

    public void SelectClosestLevel()
    {
        GetAllXPos();

        float smallestDist = Mathf.Infinity;
        int index = -1;
        float currentContentXPos = chapterSelectContainerRect.anchoredPosition.x;
        
        for (int i = 0; i < levelsXPos.Count; i++)
        {
            float levelXPos = levelsXPos[i];
            float distFromCenter = Mathf.Abs(currentContentXPos - -(levelXPos - firstLevelXPos));
            //print(currentContentXPos.ToString() + " " + levelXPos.ToString() + " " + firstLevelXPos.ToString());
            if (smallestDist > distFromCenter)
            {
                smallestDist = distFromCenter;
                index = i;
            }
        }

        currentChapterSelected = spawnedChapters[index];

        SelectChapterLerp(index + 1);
    }

    void GetAllXPos()
    {
        levelsXPos.Clear();
        foreach (ChapterPreview lp in spawnedChapters)
        {
            float xAnchorPos = lp.chapterObject.GetComponent<RectTransform>().anchoredPosition.x;
            if (lp.chapter == 1)
            {
                firstLevelXPos = xAnchorPos;
            }
            else if (lp.chapter == 2)
            {
                secondLevelXPos = xAnchorPos;
                xPosDiff = secondLevelXPos - firstLevelXPos;
                xPosDiffHalf = xPosDiff / 2;
            }

            levelsXPos.Add(xAnchorPos);
        }
    }

    public void SpawnAllLevels()
    {
        Vector2 startingPos = new Vector2( Screen.width / 2f, 0);
        int i = 1;
        foreach (ChapterPreview lp in chapterPreviews)
        {
            GameObject o = Instantiate(lp.chapterObject, chapterSelectContainer);
            o.GetComponent<RectTransform>().anchoredPosition = startingPos + new Vector2(spacingBetweenChapters * (i-1), 0);
            ChapterPreview spawnedLP = lp;
            spawnedLP.chapterObject = o;

            if (i > 1 && i > completedChapter + 1)
            {
                Transform lockLevel = Instantiate(chapterLockedPrefab, o.transform);
                
                lockLevel.SetAsLastSibling();
                spawnedLP.lockObject = lockLevel;
            }

            spawnedChapters.Add(spawnedLP);
            
            i++;
        }
        //Can't seem to get correct values here, so must call somewhere else
        //GetAllXPos();

        RectTransform levelSelectRect = chapterSelectContainer.GetComponent<RectTransform>();
        levelSelectRect.sizeDelta = new Vector2(spacingBetweenChapters * (i - 1), 0);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("VerticalLevel");
    }

    public void Level3()
    {
        SceneManager.LoadScene("");
    }

    public void LevelEdit()
    {
        SceneManager.LoadScene("LevelEdit");
    }

    public void RoomSelect()
    {
        SceneManager.LoadScene("RoomSelect");
    }

    public void EditorAddChapter()
    {
        foreach(ChapterPreview l in chapterPreviews)
        {
            if (l.chapter == editorChapterNumber)
            {
                chapterPreviews.Remove(l);
                break;
            }
        }

        chapterPreviews.Add(new ChapterPreview(editorChapterNumber, editorChapterObject));
        SortChapters();
    }

    public void EditorDeleteChapter()
    {
        foreach (ChapterPreview l in chapterPreviews)
        {
            if (l.chapter == editorChapterNumber)
            {
                chapterPreviews.Remove(l);
                break;
            }
        }
        SortChapters();
    }

    public void SortChapters()
    {
        chapterPreviews.Sort((a,b ) => a.chapter.CompareTo(b.chapter));
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey(saveDataName);
    }

}


[System.Serializable]
public class ChapterPreview
{
    public int chapter;
    public GameObject chapterObject;
    public Transform lockObject;

    public ChapterPreview(int l, GameObject lo = null)
    {
        chapter = l;
        chapterObject = lo;
    }
}