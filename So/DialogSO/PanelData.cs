
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelData
{
    public List<ConditionWithIndex> conditionsWhithIndex;
    public int nameIndex;
    public int audioIndex;
    public string sentens;
    public Sprite photo;
    public List<Choise> choice;
}