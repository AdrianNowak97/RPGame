using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemSO : ScriptableObject
{
	new public string name = "New Item";
	public Sprite icon = null;
    [TextArea(3,20)]
    public string opis;
	public GameObject prefabItemu;
	public List<ConditionDesctiption> conditionDesctiptions = new List<ConditionDesctiption>();
}




