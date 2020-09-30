using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class BuildEditDataRDS : MonoBehaviour
{
    //Store object index.  1 - Plant, 2 - Table, 3 - Sofa1, 4 - Sofa2
    static int[] buildEditObjectsIndex;

    //Store object prefabs to be instantiated
    public GameObject[] objectPrefabs;

    public GameObject[] objects;

    //Indexing for save slots
    public static int slotIndex = 0;

    public string[] dataArray;
    bool firstLoad = true;
    public bool PlayOnAwake = false;
    public string prevRandom = null;

    public GameObject loadingPanel;
    public GameObject savingPanel;
    public GameObject NormalPanel;

    private void Start()
    {
        ConnectDB.StartConnection();
        if (PlayOnAwake)
        {
            PullDB();
        }
        loadingPanel.SetActive(false);
    }

    public void Save()
    {
        if (CheckCollision(GetObjects()))
        {
            Debug.Log("Collision detected");
            return;
        }

        if(GameObject.FindGameObjectsWithTag("Teddy Bear").Length <= 0)
        {
            StartCoroutine(SetActiveDelay(NormalPanel));
            return;
        }
        GameObject[] buildEditObjects = GetObjects();

        buildEditObjectsIndex = new int[buildEditObjects.Length];

        ConnectDB.Insert();
        for (int i = 0; i < buildEditObjects.Length; i++)
        {
            if (buildEditObjects[i].name.Contains("Plant"))
            {
                buildEditObjectsIndex[i] = 0;
            }
            else if (buildEditObjects[i].name.Contains("Table"))
            {
                buildEditObjectsIndex[i] = 1;
            }
            else if (buildEditObjects[i].name.Contains("Sofa1"))
            {
                buildEditObjectsIndex[i] = 2;
            }
            else if (buildEditObjects[i].name.Contains("Sofa2"))
            {
                buildEditObjectsIndex[i] = 3;
            }
            else if (buildEditObjects[i].name.Contains("Teddy"))
            {
                buildEditObjectsIndex[i] = 4;
            }
            ConnectDB.InsertData(buildEditObjectsIndex[i], buildEditObjects[i].transform.position.x, buildEditObjects[i].transform.position.z, buildEditObjects[i].transform.eulerAngles.y);
        }
        StartCoroutine(SetActiveDelay(savingPanel));
    }

    public void WipeDB()
    {
        ConnectDB.Delete();
    }

    public void PullDB()
    {
        string CurrRandom = ConnectDB.Pull();
        if (CurrRandom == null || CurrRandom == "")
        {
            return;
        }
        if (CurrRandom != prevRandom)
        {
            Clear();

            prevRandom = CurrRandom;
            string[] file = CurrRandom.Split('|');
            GameObject currentLoadedObject = null;
            Vector3 currentObjectPos = new Vector3();
            int currentObjectRot = 0;

            int valCounter = 1;

            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] == null || file[i] == "")
                {
                    return;
                }
                if (valCounter == 1)

                {
                    currentLoadedObject = Instantiate(objects[int.Parse(file[i])]);
                }

                if (valCounter == 2)
                {
                    currentObjectPos.x = float.Parse(file[i]);
                }

                if (valCounter == 3)
                {
                    currentObjectPos.z = float.Parse(file[i]);
                }

                if (valCounter == 4)
                {
                    currentObjectRot = int.Parse(file[i]);
                    valCounter = 0;
                }
                currentLoadedObject.transform.position = new Vector3(currentObjectPos.x, currentLoadedObject.GetComponent<Renderer>().bounds.size.y / 2, currentObjectPos.z);
                currentLoadedObject.transform.rotation = Quaternion.Euler(0, currentObjectRot, 0);
                valCounter++;
            }
        }
        else if(CurrRandom == prevRandom)
        {
            PullDB();
        }
    }

    GameObject[] GetObjects()
    {
        LevelEditDraggable[] draggables = FindObjectsOfType<LevelEditDraggable>();
        GameObject[] objects = new GameObject[draggables.Length];
        for (int i = 0; i < objects.Length; i++)
            objects[i] = draggables[i].gameObject;
        return objects;
    }

    bool CheckCollision(GameObject[] arrayToCheck)
    {
        for (int i = 0; i < arrayToCheck.Length; i++)
        {
            if (arrayToCheck[i].GetComponent<LevelEditDraggable>().collided)
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        GameObject[] objectToDestroy = GetObjects();
        for (int i = 0; i < objectToDestroy.Length; i++)
        {
            objectToDestroy[i].transform.position = new Vector3(0, -10, 0);
            StartCoroutine(DeleteDelay(objectToDestroy[i]));
        }
    }

    IEnumerator SetActiveDelay(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    //Delay so that when destroying object, grid pieces can detect onTriggerExit
    IEnumerator DeleteDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

    //#region Save and load without ggp

    ////Store the objects
    //static GameObject[] buildEditObjectsSlot1;
    //static GameObject[] buildEditObjectsSlot2;
    //static GameObject[] buildEditObjectsSlot3;

    ////Store object index.  1 - Plant, 2 - Table, 3 - Sofa1, 4 - Sofa2
    //static int[] buildEditObjectsIndexSlot1;
    //static int[] buildEditObjectsIndexSlot2;
    //static int[] buildEditObjectsIndexSlot3;

    ////Store position of objects
    //static float[] buildEditObjectPositionsXSlot1;
    //static float[] buildEditObjectPositionsZSlot1;
    //static float[] buildEditObjectPositionsXSlot2;
    //static float[] buildEditObjectPositionsZSlot2;
    //static float[] buildEditObjectPositionsXSlot3;
    //static float[] buildEditObjectPositionsZSlot3;

    ////Store rotation of objects
    //static float[] buildEditObjectRotationsSlot1;
    //static float[] buildEditObjectRotationsSlot2;
    //static float[] buildEditObjectRotationsSlot3;

    ////public void SaveOnSave()
    ////{
    ////    if(ConnectDB.StartConnection())
    ////    {
    ////        Save();
    ////    }
    ////    else
    ////    {
    ////        //Save offline
    ////    }
    ////}

    ////public void LoadOffline()
    ////{
    ////    if (slotIndex == 1)
    ////    {
    ////        LoadSlot1();
    ////    }

    ////    if (slotIndex == 2)
    ////    {
    ////        LoadSlot2();
    ////    }

    ////    if (slotIndex == 3)
    ////    {
    ////        LoadSlot3();
    ////    }
    ////}

    ////public void SaveOffline()
    ////{
    ////    if (slotIndex == 1)
    ////    {
    ////        SaveSlot1();
    ////    }

    ////    if (slotIndex == 2)
    ////    {
    ////        SaveSlot2();
    ////    }

    ////    if (slotIndex == 3)
    ////    {
    ////        SaveSlot3();
    ////    }
    ////}

    ////public void SaveSlot()
    ////{
    ////    if (CheckCollision(GetObjects()))
    ////    {
    ////        return;
    ////    }
    ////    buildEditObjectsSlot1 = GetObjects();
    ////    GameObject[] buildEditObjects = GetObjects();

    ////    buildEditObjectsIndexSlot1 = new int[buildEditObjectsSlot1.Length];
    ////    buildEditObjectPositionsXSlot1 = new float[buildEditObjectsSlot1.Length];
    ////    buildEditObjectPositionsZSlot1 = new float[buildEditObjectsSlot1.Length];
    ////    buildEditObjectRotationsSlot1 = new float[buildEditObjectsSlot1.Length];

    ////    for (int i = 0; i < buildEditObjectsSlot1.Length; i++)
    ////    {
    ////        if (buildEditObjectsSlot1[i].name.Contains("Plant"))
    ////        {
    ////            buildEditObjectsIndexSlot1[i] = 0;
    ////        }
    ////        else if (buildEditObjectsSlot1[i].name.Contains("Table"))
    ////        {
    ////            buildEditObjectsIndexSlot1[i] = 1;
    ////        }
    ////        else if (buildEditObjectsSlot1[i].name.Contains("Sofa1"))
    ////        {
    ////            buildEditObjectsIndexSlot1[i] = 2;
    ////        }
    ////        else if (buildEditObjectsSlot1[i].name.Contains("Sofa2"))
    ////        {
    ////            buildEditObjectsIndexSlot1[i] = 3;
    ////        }

    ////        buildEditObjectPositionsXSlot1[i] = buildEditObjectsSlot1[i].transform.position.x;
    ////        buildEditObjectPositionsZSlot1[i] = buildEditObjectsSlot1[i].transform.position.z;

    ////        buildEditObjectRotationsSlot1[i] = buildEditObjectsSlot1[i].transform.eulerAngles.y;
    ////    }
    ////}
    //#endregion
}
