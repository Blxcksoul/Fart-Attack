using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(TextureLoader))]
public class TextureLoaderEditor : Editor
{
    SerializedProperty chapterNumber;
    SerializedProperty objRef;
    SerializedProperty textureSetsForObj;
    SerializedProperty checkSpecificChapter;
    SerializedProperty setAsASet;


    private void OnEnable()
    {
        chapterNumber = serializedObject.FindProperty("editorChapterNumber");
        objRef = serializedObject.FindProperty("editorObjRef");
        textureSetsForObj = serializedObject.FindProperty("editorObjTextures");
        checkSpecificChapter = serializedObject.FindProperty("editorCheckSpecificChapter");
        setAsASet = serializedObject.FindProperty("editorSetAsASet");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PrefixLabel("Texture Loader Editor");

        EditorGUILayout.PropertyField(chapterNumber, new GUIContent("Chapter Number"));
        EditorGUILayout.PropertyField(checkSpecificChapter, new GUIContent("Search texture sets with specific chapter"));
        EditorGUILayout.PropertyField(setAsASet, new GUIContent("Set as a set"));


        TextureLoader s = (TextureLoader)target;

        EditorGUILayout.Space();

        EditorGUILayout.PrefixLabel("Object Mesh Reference");
        EditorGUILayout.PropertyField(objRef);
        EditorGUILayout.PropertyField(textureSetsForObj);

        if (GUILayout.Button("Add New Random Texture", GUILayout.MaxHeight(20)))
        {
            textureSetsForObj.InsertArrayElementAtIndex(textureSetsForObj.arraySize);
        }

        EditorGUILayout.Space();

        for (int i = 0; i < textureSetsForObj.arraySize; i++)
        {
            EditorGUILayout.PropertyField(textureSetsForObj.GetArrayElementAtIndex(i));
            //Remove object texture set
            if (GUILayout.Button("Remove", GUILayout.MaxHeight(15)))
            {
                textureSetsForObj.DeleteArrayElementAtIndex(i);
            }
            
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Set Textures For Chapter"))
        {
            s.EditorSetTexturesForChapter();
        }

        if (GUILayout.Button("Sort"))
        {
            s.SortChapters();
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        serializedObject.ApplyModifiedProperties();

        if (s.editorCheckSpecificChapter == true)
        {
            EditorGUILayout.LabelField("Displaying all existing texture sets related to chapter: ");
            EditorGUILayout.Space();
            int wa = 0;
            foreach (TextureSets l in s.textureSets)
            {
                if (l.chapter == s.editorChapterNumber)
                {
                    EditorGUILayout.PrefixLabel("Chapter: " + l.chapter.ToString());
                    EditorGUILayout.LabelField("Set as a set : " + l.setAsASet);
                    EditorGUILayout.ObjectField("Obj Ref ", l.objRef, typeof(Mesh), true);

                    int a = 0;
                    foreach (Texture2D t in l.textures)
                    {

                        EditorGUILayout.ObjectField("Texture " + a, t, typeof(Texture2D), true);
                        a++;
                    }
                    if (GUILayout.Button("Remove", GUILayout.MaxHeight(20)))
                    {
                        s.textureSets.RemoveAt(wa);
                    }
                }
                wa++;
            }

            return;
        }

        if (s.editorObjRef == null)
        {
            EditorGUILayout.LabelField("Displaying all existing texture sets: ");
            EditorGUILayout.Space();
            int wa = 0;
            foreach (TextureSets l in s.textureSets)
            {
                EditorGUILayout.PrefixLabel("Chapter: " + l.chapter.ToString());
                EditorGUILayout.LabelField("Set as a set : " + l.setAsASet);
                EditorGUILayout.ObjectField("Obj Ref ", l.objRef, typeof(Mesh), true);

                int a = 0;
                foreach (Texture2D t in l.textures)
                {

                    EditorGUILayout.ObjectField("Texture " + a, t, typeof(Texture2D), true);
                    a++;
                }
                if (GUILayout.Button("Remove", GUILayout.MaxHeight(20)))
                {
                    s.textureSets.RemoveAt(wa);
                }
                wa++;
            }
        }
        else
        {
            bool atLeastOne = false;
            int wa = 0;
            foreach (TextureSets l in s.textureSets)
            {
                if (s.editorObjRef == l.objRef)
                {
                    atLeastOne = true;
                    EditorGUILayout.PrefixLabel("Chapter: " + l.chapter.ToString());
                    EditorGUILayout.LabelField("Set as a set : " + l.setAsASet);
                    EditorGUILayout.ObjectField("Obj Ref ", l.objRef, typeof(Mesh), true);

                    int a = 0;
                    foreach (Texture2D t in l.textures)
                    {
                        EditorGUILayout.ObjectField("Texture " + a, t, typeof(Texture2D), true);
                        a++;
                    }
                    if (GUILayout.Button("Remove", GUILayout.MaxHeight(20)))
                    {
                        s.textureSets.RemoveAt(wa);
                    }
                }

                wa++;
            }

            if (atLeastOne)
            {
                EditorGUILayout.LabelField("All these are the existing texture sets");
            }
            else
            {
                EditorGUILayout.LabelField("No existing texture sets related to Obj Ref can be found");
            }
        }
    }
}
#endif
