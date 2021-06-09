#if UNITY_EDITOR

/* 
 * author: https://github.com/AdrianNowak97
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueSO))]
public class DialogueEditor : CustomSOEditor
{

    ProgresSO progresSO;
    SoundsSO soundsSO;
    int? selectedPropertyIndex;
    int? selectedDialogIndex;
    string[] conditionNames;
    string[] namesTabel;
    string[] audioNamesTabel;


    //DialogueSO
    SerializedProperty names;
    SerializedProperty panels;
    SerializedProperty selectedPropertyIndexProperty;
    SerializedProperty isPropertySelected;
    //PanelData
    SerializedProperty selectedProperty;
    SerializedProperty conditionsWhithIndex;
    SerializedProperty nameIndex;
    SerializedProperty audioIndex;
    SerializedProperty sentens;
    SerializedProperty photo;
    SerializedProperty choice;

    //Choice
    SerializedProperty choiseText;
    SerializedProperty choiseConditionsWhithIndex;
    SerializedProperty choiseResultConditionWhithIndex;


    void OnEnable()
    {
        names = serializedObject.FindProperty("names");
        panels = serializedObject.FindProperty("panels");
        progresSO = Resources.Load<ProgresSO>("Progres");
        soundsSO = Resources.Load<SoundsSO>("Sounds");
        namesTabel = SerializedPropertyStringListToTabel(names);
        GetSelectedProperty();
    }


    public override void OnInspectorGUI()
    {
        #region Names
        EditorGUILayout.LabelField("Postacie", EditorStyles.boldLabel);
        GUI.backgroundColor = backgroundColor;
        GUILayout.BeginVertical("Box");
        GUI.backgroundColor = Color.white;
        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < names.arraySize; i++)
        {
            if (i == 0) { GUI.enabled = false; }
            EditorGUILayout.PropertyField(names.GetArrayElementAtIndex(i), GUIContent.none);
            if (i == 0) { GUI.enabled = true; }
        }
        DrawPlusMinus(names, 1, bigButtonHeight);

        if (EditorGUI.EndChangeCheck())
        {
            namesTabel = SerializedPropertyStringListToTabel(names);
        }
        GUILayout.EndVertical();
        #endregion

        EditorGUILayout.Space();

        #region PaneleDialogowe
        EditorGUILayout.LabelField("Panele dialogowe", EditorStyles.boldLabel);
        GUI.backgroundColor = backgroundColor;
        GUILayout.BeginHorizontal("Box");
        GUI.backgroundColor = defoulBackgroundColor;
        EditorGUI.BeginChangeCheck();
        selectedPropertyIndex = DrawButtonMenu(panels, selectedPropertyIndex);
        if (EditorGUI.EndChangeCheck())
        {
            selectedDialogIndex = null;
        }

        GUILayout.BeginVertical();
        if (selectedPropertyIndex != null)
        {
            selectedProperty = panels.GetArrayElementAtIndex((int)selectedPropertyIndex);
            conditionNames = GetProgresVarablesNames();
            conditionsWhithIndex = selectedProperty.FindPropertyRelative("conditionsWhithIndex");
            nameIndex = selectedProperty.FindPropertyRelative("nameIndex");
            audioIndex = selectedProperty.FindPropertyRelative("audioIndex");
            sentens = selectedProperty.FindPropertyRelative("sentens");
            photo = selectedProperty.FindPropertyRelative("photo");
            choice = selectedProperty.FindPropertyRelative("choice");

            EditorGUILayout.LabelField("Warunki", EditorStyles.boldLabel);
            GUILayout.BeginVertical("Box");
            DrawConditionsWithButtons(conditionsWhithIndex, "==", conditionNames, progresSO);
            GUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Ogólne", EditorStyles.boldLabel);
            GUILayout.BeginVertical("Box");
            nameIndex.intValue = EditorGUILayout.Popup("Imię", nameIndex.intValue, namesTabel);

            audioNamesTabel = GetSoundsNames();
            audioIndex.intValue = EditorGUILayout.Popup("Audio", audioIndex.intValue, audioNamesTabel);
            
            if (nameIndex.intValue == 0)
            {
                EditorGUILayout.PropertyField(photo, new GUIContent("Zdjęcie"));
            }


            EditorGUILayout.LabelField("Wypowiedź");
            GUIStyle style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;
            sentens.stringValue = EditorGUILayout.TextArea(sentens.stringValue, style);
            GUILayout.EndVertical();

            if (nameIndex.intValue == 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Opcje dialogowe", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal("Box");
                selectedDialogIndex = DrawButtonMenu(choice, selectedDialogIndex);

                if (selectedDialogIndex != null)
                {
                    choiseText = choice.GetArrayElementAtIndex((int)selectedDialogIndex).FindPropertyRelative("choiseText");
                    choiseConditionsWhithIndex = choice.GetArrayElementAtIndex((int)selectedDialogIndex).FindPropertyRelative("choiseConditionsWhithIndex");
                    choiseResultConditionWhithIndex = choice.GetArrayElementAtIndex((int)selectedDialogIndex).FindPropertyRelative("choiseResultConditionWhithIndex");

                    GUILayout.BeginVertical();
                    GUILayout.Label("Warunki");
                    GUI.backgroundColor = backgroundColor2;
                    GUILayout.BeginVertical("Box");
                    GUI.backgroundColor = defoulBackgroundColor;
                    DrawConditionsWithButtons(choiseConditionsWhithIndex, "==", conditionNames, progresSO);
                    GUILayout.EndVertical();
                    EditorGUILayout.Space();
                    GUILayout.Label("Tekst opcji");
                    GUI.backgroundColor = backgroundColor2;
                    GUILayout.BeginVertical("Box");
                    GUI.backgroundColor = defoulBackgroundColor;
                    choiseText.stringValue = EditorGUILayout.TextArea(choiseText.stringValue, GUILayout.Height(40));
                    GUILayout.EndVertical();
                    EditorGUILayout.Space();
                    GUILayout.Label("Wpływ");
                    GUI.backgroundColor = backgroundColor2;
                    GUILayout.BeginVertical("Box");
                    GUI.backgroundColor = defoulBackgroundColor;
                    DrawConditionsWithButtons(choiseResultConditionWhithIndex, "=", conditionNames, progresSO);
                    GUILayout.EndVertical();
                    EditorGUILayout.Space();

                    GUILayout.EndVertical();

                }
                else if (choice.arraySize > 0)
                {
                    GUILayout.Label("Wybierz opcję dialogowe");
                }
                else
                {
                    GUILayout.Label("Brak opcji dialogowych");
                }
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.Label("Wybierz panel dialogowy");
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        #endregion

        serializedObject.ApplyModifiedProperties();

    }

    private string[] SerializedPropertyStringListToTabel(SerializedProperty serializedProperty)
    {
        string[] namesTabel = new string[serializedProperty.arraySize];
        for (int i = 0; i < namesTabel.Length; i++)
            namesTabel[i] = serializedProperty.GetArrayElementAtIndex(i).stringValue;
        return namesTabel;
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

    private string[] GetSoundsNames()
    {
        List<CharacterSounds> sounds= soundsSO.characterSounds;
        string[] soundsNames = new string[sounds.Count];

        for (int i = 0; i < soundsNames.Length; i++)
        {
            soundsNames[i] = sounds[i].name;
        }
        return soundsNames;
    }

    public void SetSelectedProperty()
    {
        if (selectedPropertyIndex == null)
        {
            isPropertySelected.boolValue = false;
        }
        else
        {
            isPropertySelected.boolValue = true;
            selectedPropertyIndexProperty.intValue = (int)selectedPropertyIndex;
        }
    }

    public void GetSelectedProperty()
    {
        isPropertySelected = serializedObject.FindProperty("isPropertySelected");
        selectedPropertyIndexProperty = serializedObject.FindProperty("selectedPropertyIndexProperty");
        if (isPropertySelected.boolValue)
        {
            selectedPropertyIndex = selectedPropertyIndexProperty.intValue;
        }
        else
        {
            selectedPropertyIndex = null;
        }
    }

    void OnDisable()
    {
        SetSelectedProperty();
        serializedObject.ApplyModifiedProperties();
    }


}
#endif