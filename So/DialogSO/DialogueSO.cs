#if UNITY_EDITOR

/* 
 * author: https://github.com/AdrianNowak97
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogSO")]
public class DialogueSO : ScriptableObject
{
    public int selectedPropertyIndexProperty;
    public bool isPropertySelected = false;
    public List<string> names = new List<string>() { "Narrator" };
    public List<PanelData> panels = new List<PanelData>();
}

#endif