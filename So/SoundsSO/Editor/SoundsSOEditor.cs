
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundsSO))]
public class SoundsSOEditor : CustomSOEditor
{
    SerializedProperty characterSounds;
    SerializedProperty audioClips;
    void OnEnable()
    {
        characterSounds = serializedObject.FindProperty("characterSounds");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Dźwięki postaci", EditorStyles.boldLabel);
        for (int i = 0; i < characterSounds.arraySize; i++)
        {
            GUILayout.BeginVertical("Box");

            if (i == 0) { GUI.enabled = false; }
            EditorGUILayout.PropertyField(characterSounds.GetArrayElementAtIndex(i).FindPropertyRelative("name"), new GUIContent("Nazwa dzwięków"));
            if (i == 0) { GUI.enabled = true; }

            EditorGUILayout.PropertyField(characterSounds.GetArrayElementAtIndex(i).FindPropertyRelative("speed"), new GUIContent("Prędkość"));

            audioClips = characterSounds.GetArrayElementAtIndex(i).FindPropertyRelative("AudioClips");
            for (int j = 0; j < audioClips.arraySize; j++)
            {
                EditorGUILayout.PropertyField(audioClips.GetArrayElementAtIndex(j), GUIContent.none);
            }
            DrawPlusMinus(characterSounds.GetArrayElementAtIndex(i).FindPropertyRelative("AudioClips"), 1, smalButtonHeight);

            GUILayout.EndVertical();
            GUILayout.Space(5);

        }
        DrawPlusMinus(characterSounds, 1, bigButtonHeight);

        serializedObject.ApplyModifiedProperties();

    }
}
