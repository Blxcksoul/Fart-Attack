using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(string _name)
    {
        if (Application.CanStreamedLevelBeLoaded(_name))
        {
            SceneManager.LoadScene(_name);
        }
        else
        {
            Debug.Log(_name + " scene is not in build settings!");
        }

    }
}
