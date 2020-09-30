using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class BuildEditData : MonoBehaviour
{
    //Store object index.  1 - Plant, 2 - Table, 3 - Sofa1, 4 - Sofa2
    static int[] buildEditObjectsIndex;

    //Store object prefabs to be instantiated
    public GameObject[] objectPrefabs;

    //Indexing for save slots
    public static  int slotIndex = 0;

    public string[] dataArray;
    bool firstLoad = true;

    public GameObject loadingPanel;

    public void Start()
    {
        if(GooglePlayAccount.instance == null)
        {
            PlayGamesPlatform.Activate();
        }

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                LoadData(slotIndex);
            }
            else
            {
                firstLoad = false;
                loadingPanel.SetActive(false);
                LoadOffline();
            }
        });
    }

    public void LateUpdate()
    {
        if(firstLoad && dataArray != GooglePlayAccount.dataArray)
        {
            loadingPanel.SetActive(false);
            LoadEdit();
            firstLoad = false;
        }
    }

    public void SaveOnSave()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Save();
            }
            else
            {
                //save without ggp
                SaveOffline();
            }
        });
    }

    public void Save()
    {
        if (CheckCollision(GetObjects()))
        {
            Debug.Log("Collision detected");
            return;
        }
        GameObject[] buildEditObjects = GetObjects();

        buildEditObjectsIndex = new int[buildEditObjects.Length];

        string stringToSave = null;

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

            stringToSave += buildEditObjectsIndex[i];
            stringToSave += "|";
            stringToSave += buildEditObjects[i].transform.position.x;
            stringToSave += "|";
            stringToSave += buildEditObjects[i].transform.position.z;
            stringToSave += "|";
            stringToSave += buildEditObjects[i].transform.eulerAngles.y;
            stringToSave += "|";
        }

        GooglePlayAccount.instance.TestVariable = stringToSave;
        GooglePlayAccount.instance.OpenSave(true, slotIndex);
    }

    public void LoadData(int slotIndexToLoad)
    {
        dataArray = GooglePlayAccount.dataArray;
        GooglePlayAccount.instance.OpenSave(false, slotIndexToLoad);
    }

    public void LoadEdit()
    {
        dataArray = GooglePlayAccount.dataArray;

        GameObject currentLoadedObject = null;
        Vector3 currentObjectPos = new Vector3();
        int currentObjectRot = 0;

        int valCounter = 1;

        for (int i = 0; i < dataArray.Length; i++)
        {
            if (valCounter == 1)
            {
                currentLoadedObject = Instantiate(objectPrefabs[int.Parse(dataArray[i])]);
            }

            if (valCounter == 2)
            {
                currentObjectPos.x = float.Parse(dataArray[i]);
            }

            if (valCounter == 3)
            {
                currentObjectPos.z = float.Parse(dataArray[i]);
            }

            if (valCounter == 4)
            {
                currentObjectRot = int.Parse(dataArray[i]);
                valCounter = 0;
            }
            currentLoadedObject.transform.position = new Vector3(currentObjectPos.x, currentLoadedObject.GetComponent<Renderer>().bounds.size.y / 2, currentObjectPos.z);
            currentLoadedObject.transform.rotation = Quaternion.Euler(0, currentObjectRot, 0);
            valCounter++;
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

    //Delay so that when destroying object, grid pieces can detect onTriggerExit
    IEnumerator DeleteDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

    #region Save and load without ggp

    //Store the objects
    static GameObject[] buildEditObjectsSlot1;
    static GameObject[] buildEditObjectsSlot2;
    static GameObject[] buildEditObjectsSlot3;

    //Store object index.  1 - Plant, 2 - Table, 3 - Sofa1, 4 - Sofa2
    static int[] buildEditObjectsIndexSlot1;
    static int[] buildEditObjectsIndexSlot2;
    static int[] buildEditObjectsIndexSlot3;

    //Store position of objects
    static float[] buildEditObjectPositionsXSlot1;
    static float[] buildEditObjectPositionsZSlot1;
    static float[] buildEditObjectPositionsXSlot2;
    static float[] buildEditObjectPositionsZSlot2;
    static float[] buildEditObjectPositionsXSlot3;
    static float[] buildEditObjectPositionsZSlot3;

    //Store rotation of objects
    static float[] buildEditObjectRotationsSlot1;
    static float[] buildEditObjectRotationsSlot2;
    static float[] buildEditObjectRotationsSlot3;

    public void LoadOffline()
    {
        if (slotIndex == 1)
        {
            LoadSlot1();
        }

        if (slotIndex == 2)
        {
            LoadSlot2();
        }

        if (slotIndex == 3)
        {
            LoadSlot3();
        }
    }

    public void SaveOffline()
    {
        if (slotIndex == 1)
        {
            SaveSlot1();
        }

        if (slotIndex == 2)
        {
            SaveSlot2();
        }

        if (slotIndex == 3)
        {
            SaveSlot3();
        }
    }

    public void SaveSlot1()
    {
        if (CheckCollision(GetObjects()))
        {
            return;
        }
        buildEditObjectsSlot1 = GetObjects();
        GameObject[] buildEditObjects = GetObjects();

        buildEditObjectsIndexSlot1 = new int[buildEditObjectsSlot1.Length];
        buildEditObjectPositionsXSlot1 = new float[buildEditObjectsSlot1.Length];
        buildEditObjectPositionsZSlot1 = new float[buildEditObjectsSlot1.Length];
        buildEditObjectRotationsSlot1 = new float[buildEditObjectsSlot1.Length];

        for (int i = 0; i < buildEditObjectsSlot1.Length; i++)
        {
            if (buildEditObjectsSlot1[i].name.Contains("Plant"))
            {
                buildEditObjectsIndexSlot1[i] = 0;
            }
            else if (buildEditObjectsSlot1[i].name.Contains("Table"))
            {
                buildEditObjectsIndexSlot1[i] = 1;
            }
            else if (buildEditObjectsSlot1[i].name.Contains("Sofa1"))
            {
                buildEditObjectsIndexSlot1[i] = 2;
            }
            else if (buildEditObjectsSlot1[i].name.Contains("Sofa2"))
            {
                buildEditObjectsIndexSlot1[i] = 3;
            }

            buildEditObjectPositionsXSlot1[i] = buildEditObjectsSlot1[i].transform.position.x;
            buildEditObjectPositionsZSlot1[i] = buildEditObjectsSlot1[i].transform.position.z;

            buildEditObjectRotationsSlot1[i] = buildEditObjectsSlot1[i].transform.eulerAngles.y;
        }
    }

    public void SaveSlot2()
    {
        if (CheckCollision(GetObjects()))
        {
            return;
        }
        buildEditObjectsSlot2 = GetObjects();

        buildEditObjectsIndexSlot2 = new int[buildEditObjectsSlot2.Length];
        buildEditObjectPositionsXSlot2 = new float[buildEditObjectsSlot2.Length];
        buildEditObjectPositionsZSlot2 = new float[buildEditObjectsSlot2.Length];
        buildEditObjectRotationsSlot2 = new float[buildEditObjectsSlot2.Length];

        for (int i = 0; i < buildEditObjectsSlot2.Length; i++)
        {
            if (buildEditObjectsSlot2[i].name.Contains("Plant"))
            {
                buildEditObjectsIndexSlot2[i] = 0;
            }
            else if (buildEditObjectsSlot2[i].name.Contains("Table"))
            {
                buildEditObjectsIndexSlot2[i] = 1;
            }
            else if (buildEditObjectsSlot2[i].name.Contains("Sofa1"))
            {
                buildEditObjectsIndexSlot2[i] = 2;
            }
            else if (buildEditObjectsSlot2[i].name.Contains("Sofa2"))
            {
                buildEditObjectsIndexSlot2[i] = 3;
            }

            buildEditObjectPositionsXSlot2[i] = buildEditObjectsSlot2[i].transform.position.x;
            buildEditObjectPositionsZSlot2[i] = buildEditObjectsSlot2[i].transform.position.z;

            buildEditObjectRotationsSlot2[i] = buildEditObjectsSlot2[i].transform.eulerAngles.y;
        }
    }

    public void SaveSlot3()
    {
        if (CheckCollision(GetObjects()))
        {
            return;
        }
        buildEditObjectsSlot3 = GetObjects();

        buildEditObjectsIndexSlot3 = new int[buildEditObjectsSlot3.Length];
        buildEditObjectPositionsXSlot3 = new float[buildEditObjectsSlot3.Length];
        buildEditObjectPositionsZSlot3 = new float[buildEditObjectsSlot3.Length];
        buildEditObjectRotationsSlot3 = new float[buildEditObjectsSlot3.Length];

        for (int i = 0; i < buildEditObjectsSlot3.Length; i++)
        {
            if (buildEditObjectsSlot3[i].name.Contains("Plant"))
            {
                buildEditObjectsIndexSlot3[i] = 0;
            }
            else if (buildEditObjectsSlot3[i].name.Contains("Table"))
            {
                buildEditObjectsIndexSlot3[i] = 1;
            }
            else if (buildEditObjectsSlot3[i].name.Contains("Sofa1"))
            {
                buildEditObjectsIndexSlot3[i] = 2;
            }
            else if (buildEditObjectsSlot3[i].name.Contains("Sofa2"))
            {
                buildEditObjectsIndexSlot3[i] = 3;
            }

            buildEditObjectPositionsXSlot3[i] = buildEditObjectsSlot3[i].transform.position.x;
            buildEditObjectPositionsZSlot3[i] = buildEditObjectsSlot3[i].transform.position.z;

            buildEditObjectRotationsSlot3[i] = buildEditObjectsSlot3[i].transform.eulerAngles.y;
        }
    }

    public void LoadSlot1()
    {
        GameObject[] objectToDestroy = GetObjects();
        for (int i = 0; i < objectToDestroy.Length; i++)
        {
            objectToDestroy[i].transform.position = new Vector3(0, -10, 0);
            StartCoroutine(DeleteDelay(objectToDestroy[i]));
        }

        if (buildEditObjectsSlot1 != null)
        {
            for (int i = 0; i < buildEditObjectsSlot1.Length; i++)
            {
                Instantiate(objectPrefabs[buildEditObjectsIndexSlot1[i]]
                    , new Vector3(buildEditObjectPositionsXSlot1[i], objectPrefabs[buildEditObjectsIndexSlot1[i]].GetComponent<Renderer>().bounds.size.y / 2, buildEditObjectPositionsZSlot1[i])
                    , Quaternion.Euler(0, buildEditObjectRotationsSlot1[i], 0));
            }
        }
    }

    public void LoadSlot2()
    {
        GameObject[] objectToDestroy = GetObjects();
        for (int i = 0; i < objectToDestroy.Length; i++)
        {
            objectToDestroy[i].transform.position = new Vector3(0, -10, 0);
            StartCoroutine(DeleteDelay(objectToDestroy[i]));
        }

        if (buildEditObjectsSlot2 != null)
        {
            if (buildEditObjectsSlot2 != null)
            {
                for (int i = 0; i < buildEditObjectsSlot2.Length; i++)
                {
                    Instantiate(objectPrefabs[buildEditObjectsIndexSlot2[i]]
                        , new Vector3(buildEditObjectPositionsXSlot2[i], objectPrefabs[buildEditObjectsIndexSlot2[i]].GetComponent<Renderer>().bounds.size.y / 2, buildEditObjectPositionsZSlot2[i])
                        , Quaternion.Euler(0, buildEditObjectRotationsSlot2[i], 0));
                }
            }
        }
    }

    public void LoadSlot3()
    {
        GameObject[] objectToDestroy = GetObjects();
        for (int i = 0; i < objectToDestroy.Length; i++)
        {
            objectToDestroy[i].transform.position = new Vector3(0, -10, 0);
            StartCoroutine(DeleteDelay(objectToDestroy[i]));
        }

        if (buildEditObjectsSlot3 != null)
        {
            if (buildEditObjectsSlot3 != null)
            {
                for (int i = 0; i < buildEditObjectsSlot3.Length; i++)
                {
                    Instantiate(objectPrefabs[buildEditObjectsIndexSlot3[i]]
                        , new Vector3(buildEditObjectPositionsXSlot3[i], objectPrefabs[buildEditObjectsIndexSlot3[i]].GetComponent<Renderer>().bounds.size.y / 2, buildEditObjectPositionsZSlot3[i])
                        , Quaternion.Euler(0, buildEditObjectRotationsSlot3[i], 0));
                }
            }
        }
    }
    #endregion
}
