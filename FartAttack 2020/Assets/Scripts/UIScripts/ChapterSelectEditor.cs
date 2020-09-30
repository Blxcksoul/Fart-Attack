using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ChapterSelectManager))]
public class ChapterSelectEditor : Editor
{
    SerializedProperty chapterNumber;
    SerializedProperty chapterObject;

    private void OnEnable()
    {
        chapterNumber = serializedObject.FindProperty("editorChapterNumber");
        chapterObject = serializedObject.FindProperty("editorChapterObject");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PrefixLabel("Chapter Selection Preview Editor");

        EditorGUILayout.PropertyField(chapterNumber, new GUIContent("Chapter Number"));
        EditorGUILayout.PropertyField(chapterObject, new GUIContent("Chapter Object"));

        ChapterSelectManager s = (ChapterSelectManager)target;

        if (GUILayout.Button("Add Chapter"))
        {
            s.EditorAddChapter();
        }

        if (GUILayout.Button("Delete Chapter"))
        {
            s.EditorDeleteChapter();
        }

        if (GUILayout.Button("Sort"))
        {
            s.SortChapters();
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        serializedObject.ApplyModifiedProperties();

        foreach (ChapterPreview l in s.chapterPreviews)
        {
            EditorGUILayout.PrefixLabel("Chapter: " + l.chapter.ToString());
            EditorGUILayout.ObjectField("Chapter Object:", l.chapterObject, typeof(GameObject), true);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
    }
}
#endif
