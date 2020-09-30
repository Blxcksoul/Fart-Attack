using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAd : MonoBehaviour
{
    //ref: https://unityads.unity3d.com/help/unity/integration-guide-unity
    //ref: https://www.youtube.com/watch?v=ayfHGvsnjoU
    static string gameId = "3421562";
    public bool testMode = false;

    static string skippableName = "video";
    static string forceName = "forceAd";

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public static void ShowSkippableAd()
    {
        Advertisement.Show(skippableName);
    }

    public static void ShowForceAd()
    {
        Advertisement.Show(forceName);
    }
}