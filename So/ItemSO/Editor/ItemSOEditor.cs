using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemSO))]
public class ItemSOEditor : CustomSOEditor
{
    ProgresSO progresSO;

    SerializedProperty name;
    SerializedProperty opis;
    SerializedProperty prefabItemu;
    SerializedProperty icon;
    SerializedProperty conditionDesctiptions;
    SerializedProperty conditionWithIndex;
    SerializedProperty description;
    string[] conditionNames;

    void OnEnable()
    {
        name = serializedObject.FindProperty("name");
        opis = serializedObject.FindProperty("opis");
        prefabItemu = serializedObject.FindProperty("prefabItemu");
        icon = serializedObject.FindProperty("icon");
        conditionDesctiptions = serializedObject.FindProperty("conditionDesctiptions");
        progresSO = Resources.Load<ProgresSO>("Progres");
        conditionNames = GetProgresVarablesNames();
        
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Podstawowe dane", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(name, new GUIContent("Name"));
        EditorGUILayout.PropertyField(prefabItemu, new GUIContent("Prefab"));
        icon.objectReferenceValue = EditorGUILayout.ObjectField("Icon", icon.objectReferenceValue, typeof(Sprite), false);
        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;
        EditorGUILayout.LabelField("Opis podstawowy");
        opis.stringValue = EditorGUILayout.TextArea(opis.stringValue, style);
        GUILayout.EndVertical();

       
        for (int i = 0; i < conditionDesctiptions.arraySize; i++)
        {
            GUILayout.Label("Warunkowy opis dodatkowy", EditorStyles.boldLabel);
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Warunki");
            GUI.backgroundColor = backgroundColor2;
            GUILayout.BeginVertical("Box");
            GUI.backgroundColor = defoulBackgroundColor;
            conditionWithIndex = conditionDesctiptions.GetArrayElementAtIndex(i).FindPropertyRelative("conditionWithIndices");
            DrawConditionsWithButtons(conditionWithIndex, "==", conditionNames, progresSO);
            GUILayout.EndVertical();
            EditorGUILayout.Space();
            GUILayout.Label("Opis");

            GUILayout.BeginVertical("Box");
            description = conditionDesctiptions.GetArrayElementAtIndex(i).FindPropertyRelative("description");
            style.wordWrap = true;
            description.stringValue = EditorGUILayout.TextArea(description.stringValue, style);
            EditorGUILayout.Space();
            GUILayout.EndVertical();
            GUILayout.EndVertical();

            EditorGUILayout.Space();
        }
        
        DrawPlusMinus(conditionDesctiptions, 0, bigButtonHeight);

        serializedObject.ApplyModifiedProperties();
    }

    private string[] GetProgresVarablesNames()
    {
        List<Condition> variables = progresSO.conditions;
        string[] varablesNames = new string[variables.Count];

        for (int i = 0; i < varablesNames.Length; i++)
        {
            varablesNames[i] = variables[i].name;
        }
        return varablesNames;
    }
}
