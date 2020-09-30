using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextureLoader : MonoBehaviour
{

    public static TextureLoader instance;
    [HideInInspector]
    public int editorChapterNumber;
    [HideInInspector]
    public Mesh editorObjRef;
    [HideInInspector]
    public Texture2D[] editorObjTextures;
    [HideInInspector]
    public bool editorCheckSpecificChapter;

    [HideInInspector]
    public bool editorSetAsASet;

    [HideInInspector]
    public List<TextureSets> textureSets = new List<TextureSets>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += RandomiseTexture;
    }

    public void RandomiseTexture(Scene s, LoadSceneMode l)
    {
        MeshFilter[] allMeshes = FindObjectsOfType<MeshFilter>();
        List<SetAsASetInfo> setAsASetIndex = new List<SetAsASetInfo>();

        foreach (MeshFilter m in allMeshes)
        {
            int tsIndex = 0;
            foreach (TextureSets ts in textureSets)
            {
                if (ts.objRef == m.sharedMesh && ChapterSelectManager.currentChapterSelected.chapter == ts.chapter)
                {
                    MeshRenderer mr = m.GetComponent<MeshRenderer>();
                    Material mrM = mr.material;

                    int randomIndex = Random.Range(0, ts.textures.Length);
                    if (ts.setAsASet)
                    {
                        bool infoAlreadyExist = false;
                        foreach (SetAsASetInfo wts in setAsASetIndex)
                        {
                            if (wts.objRef == ts.objRef)
                            {
                                mrM.mainTexture = wts.tex;
                                infoAlreadyExist = true;
                                break;
                            }
                        }

                        if (infoAlreadyExist)
                        {
                            break;
                        }
                        setAsASetIndex.Add(new SetAsASetInfo(ts.objRef, ts.textures[randomIndex]));
                    }
                    Texture2D randomTexture = ts.textures[randomIndex];

                    mrM.mainTexture = randomTexture;
                }

                tsIndex++;
            }
        }
    }

    public void EditorSetTexturesForChapter()
    {
        if (editorObjRef == null)
        {
            print("Object Reference is empty! Please choose a Mesh for the program to relate to for texture randomisation");
            return;
        }

        if (editorObjTextures.Length < 1)
        {
            print("Please provide at least 1 texture for the Mesh to be loaded with");
            return;
        }

        foreach (TextureSets l in textureSets)
        {
            if (l.chapter == editorChapterNumber && l.objRef == editorObjRef)
            {
                textureSets.Remove(l);
                break;
            }
        }

        Texture2D[] noRef = new Texture2D[editorObjTextures.Length];

        for (int i = 0; i < noRef.Length; i++)
        {
            noRef[i] = editorObjTextures[i];
        }

        
        textureSets.Add(new TextureSets(editorChapterNumber, editorObjRef, noRef, editorSetAsASet));
        SortChapters();
    }

    public void SortChapters()
    {
        textureSets.Sort((a, b) => a.chapter.CompareTo(b.chapter));
    }
}


[System.Serializable]
public class TextureSets
{
    public int chapter;
    public Mesh objRef;
    public Texture2D[] textures;
    public bool setAsASet;

    public TextureSets(int c, Mesh o, Texture2D[] t, bool s)
    {
        chapter = c;
        objRef = o;
        textures = t;
        setAsASet = s;
    }
}

public class SetAsASetInfo
{
    public Mesh objRef;
    public Texture2D tex;

    public SetAsASetInfo(Mesh r, Texture2D t)
    {
        objRef = r;
        tex = t;
    }
}