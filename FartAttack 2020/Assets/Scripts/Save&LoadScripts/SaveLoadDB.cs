using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadDB : MonoBehaviour
{
    //Store the objects
    GameObject[] buildEditObjectsSlot1;
    GameObject[] buildEditObjectsSlot2;
    GameObject[] buildEditObjectsSlot3;

    //Store object index.  1 - Plant, 2 - Table, 3 - Sofa1, 4 - Sofa2
    int[] buildEditObjectsIndexSlot1;
    int[] buildEditObjectsIndexSlot2;
    int[] buildEditObjectsIndexSlot3;

    //Store position of objects
    float[] buildEditObjectPositionsXSlot1;
    float[] buildEditObjectPositionsZSlot1;
    float[] buildEditObjectPositionsXSlot2;
    float[] buildEditObjectPositionsZSlot2;
    float[] buildEditObjectPositionsXSlot3;
    float[] buildEditObjectPositionsZSlot3;

    //Store rotation of objects
    float[] buildEditObjectRotationsSlot1;
    float[] buildEditObjectRotationsSlot2;
    float[] buildEditObjectRotationsSlot3;

    //Store object prefabs to be instantiated
    public GameObject[] objectPrefabs;  

    public void SaveSlot1()
    {
        if (CheckCollision(GetObjects()))
        {
            Debug.Log("Collision detected");
            return;
        }
        buildEditObjectsSlot1 = GetObjects();

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
        StartCoroutine(SaveToDB1());
    }

    IEnumerator SaveToDB1()
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < buildEditObjectsSlot1.Length; i++)
        {
            form.AddField("objects", buildEditObjectsIndexSlot1.Length);
            form.AddField("index" + i, buildEditObjectsIndexSlot1[i]);
            form.AddField("positionX" + i, buildEditObjectPositionsXSlot1[i].ToString());
            form.AddField("positionZ" + i, buildEditObjectPositionsZSlot1[i].ToString());
            form.AddField("rotation" + i, buildEditObjectRotationsSlot1[i].ToString());
        }

        WWW www = new WWW("http://localhost/saveloadSql/saveData.php", form);
        yield return www;

        Debug.Log(www.text);

    }

    public void SaveSlot2()
    {
        if (CheckCollision(GetObjects()))
        {
            Debug.Log("Collision detected");
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
        StartCoroutine(SaveToDB2());
    }

    IEnumerator SaveToDB2()
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < buildEditObjectsSlot2.Length; i++)
        {
            form.AddField("objects", buildEditObjectsIndexSlot2.Length);
            form.AddField("index" + i, buildEditObjectsIndexSlot2[i]);
            form.AddField("positionX" + i, buildEditObjectPositionsXSlot2[i].ToString());
            form.AddField("positionZ" + i, buildEditObjectPositionsZSlot2[i].ToString());
            form.AddField("rotation" + i, buildEditObjectRotationsSlot2[i].ToString());
        }

        WWW www = new WWW("http://localhost/saveloadSql/saveData2.php", form);
        yield return www;

        Debug.Log(www.text);

    }

    public void SaveSlot3()
    {
        if (CheckCollision(GetObjects()))
        {
            Debug.Log("Collision detected");
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
        StartCoroutine(SaveToDB3());
    }

    IEnumerator SaveToDB3()
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < buildEditObjectsSlot3.Length; i++)
        {
            form.AddField("objects", buildEditObjectsIndexSlot3.Length);
            form.AddField("index" + i, buildEditObjectsIndexSlot3[i]);
            form.AddField("positionX" + i, buildEditObjectPositionsXSlot3[i].ToString());
            form.AddField("positionZ" + i, buildEditObjectPositionsZSlot3[i].ToString());
            form.AddField("rotation" + i, buildEditObjectRotationsSlot3[i].ToString());
        }

        WWW www = new WWW("http://localhost/saveloadSql/saveData3.php", form);
        yield return www;

        Debug.Log(www.text);

    }

    public void LoadSlot1()
    {
        GameObject[] objectToDestroy = GetObjects();
        for (int i = 0; i < objectToDestroy.Length; i++)
        {
            objectToDestroy[i].transform.position = new Vector3(0, -10, 0);
            StartCoroutine(DeleteDelay(objectToDestroy[i]));
        }

        StartCoroutine(LoadFromDB());
    }

    IEnumerator LoadFromDB()
    {
        WWW request = new WWW("http://localhost/saveloadSql/loadData.php");
        yield return request;

        int valCounter = 1;

        GameObject currentLoadedObject = null;
        Vector3 currentObjectPos = new Vector3();
        int currentObjectRot = 0;

        string[] webResults = request.text.Split(',');
        for (int i = 0; i < webResults.Length - 1; i++)
        {
            if (valCounter == 1)
            {
                currentLoadedObject = Instantiate(objectPrefabs[int.Parse(webResults[i])]);
            }

            if (valCounter == 2)
            {
                currentObjectPos.x = float.Parse(webResults[i]);
            }

            if (valCounter == 3)
            {
                currentObjectPos.z = float.Parse(webResults[i]);
            }

            if (valCounter == 4)
            {
                currentObjectRot = int.Parse(webResults[i]);
                valCounter = 0;
            }
            currentLoadedObject.transform.position = new Vector3(currentObjectPos.x, currentLoadedObject.GetComponent<Renderer>().bounds.size.y / 2, currentObjectPos.z);
            currentLoadedObject.transform.rotation = Quaternion.Euler(0, currentObjectRot, 0);
            valCounter++;
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

        StartCoroutine(LoadFromDB2());
    }

    IEnumerator LoadFromDB2()
    {
        WWW request = new WWW("http://localhost/saveloadSql/loadData2.php");
        yield return request;

        int valCounter = 1;

        GameObject currentLoadedObject = null;
        Vector3 currentObjectPos = new Vector3();
        int currentObjectRot = 0;

        string[] webResults = request.text.Split(',');
        for (int i = 0; i < webResults.Length - 1; i++)
        {
            if (valCounter == 1)
            {
                currentLoadedObject = Instantiate(objectPrefabs[int.Parse(webResults[i])]);
            }

            if (valCounter == 2)
            {
                currentObjectPos.x = float.Parse(webResults[i]);
            }

            if (valCounter == 3)
            {
                currentObjectPos.z = float.Parse(webResults[i]);
            }

            if (valCounter == 4)
            {
                currentObjectRot = int.Parse(webResults[i]);
                valCounter = 0;
            }
            currentLoadedObject.transform.position = new Vector3(currentObjectPos.x, currentLoadedObject.GetComponent<Renderer>().bounds.size.y / 2, currentObjectPos.z);
            currentLoadedObject.transform.rotation = Quaternion.Euler(0, currentObjectRot, 0);
            valCounter++;
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

        StartCoroutine(LoadFromDB3());
    }

    IEnumerator LoadFromDB3()
    {
        WWW request = new WWW("http://localhost/saveloadSql/loadData3.php");
        yield return request;

        int valCounter = 1;

        GameObject currentLoadedObject = null;
        Vector3 currentObjectPos = new Vector3();
        int currentObjectRot = 0;

        string[] webResults = request.text.Split(',');
        for (int i = 0; i < webResults.Length - 1; i++)
        {
            if (valCounter == 1)
            {
                currentLoadedObject = Instantiate(objectPrefabs[int.Parse(webResults[i])]);
            }

            if (valCounter == 2)
            {
                currentObjectPos.x = float.Parse(webResults[i]);
            }

            if (valCounter == 3)
            {
                currentObjectPos.z = float.Parse(webResults[i]);
            }

            if (valCounter == 4)
            {
                currentObjectRot = int.Parse(webResults[i]);
                valCounter = 0;
            }
            currentLoadedObject.transform.position = new Vector3(currentObjectPos.x, currentLoadedObject.GetComponent<Renderer>().bounds.size.y / 2, currentObjectPos.z);
            currentLoadedObject.transform.rotation = Quaternion.Euler(0, currentObjectRot, 0);
            valCounter++;
        }
    }

    GameObject[] GetUndraggableObjects()
    {
        GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> destroyOnLoadList = new List<GameObject>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].layer == 12)
            {
                destroyOnLoadList.Add(objects[i]);
            }
        }
        GameObject[] destroyOnLoad = destroyOnLoadList.ToArray();
        return destroyOnLoad;
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
        //GameObject[] unDraggableObjectToDestroy = GetUndraggableObjects();
        //for (int i = 0; i < unDraggableObjectToDestroy.Length; i++)
        //{
        //    unDraggableObjectToDestroy[i].transform.position = new Vector3(0, -10, 0);
        //    StartCoroutine(DeleteDelay(unDraggableObjectToDestroy[i]));
        //}
    }

    //Delay so that when destroying object, grid pieces can detect onTriggerExit
    IEnumerator DeleteDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }
}
