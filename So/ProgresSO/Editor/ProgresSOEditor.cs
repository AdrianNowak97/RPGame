#if UNITY_EDITOR

/* 
 * author: https://github.com/AdrianNowak97
 */

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProgresSO))]
public class ProgresEditor : CustomSOEditor
{
    SerializedProperty conditions;

    void OnEnable()
    {
        conditions = serializedObject.FindProperty("conditions");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Zmienne", EditorStyles.boldLabel);
        for (int i = 0; i < conditions.arraySize; i++)
        {
            GUILayout.BeginHorizontal("Box");

            EditorGUILayout.PropertyField(conditions.GetArrayElementAtIndex(i).FindPropertyRelative("isFulfil"), GUIContent.none);
            EditorGUILayout.PropertyField(conditions.GetArrayElementAtIndex(i).FindPropertyRelative("name"), GUIContent.none);

            GUILayout.EndHorizontal();
        }
        DrawPlusMinus(conditions, 0, bigButtonHeight);

        serializedObject.ApplyModifiedProperties();

    }
}
#endif