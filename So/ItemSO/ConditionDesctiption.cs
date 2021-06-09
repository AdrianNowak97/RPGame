using System;
using System.Collections.Generic;

[Serializable]
public class ConditionDesctiption
{
	public List<ConditionWithIndex> conditionWithIndices = new List<ConditionWithIndex>();
	public string description;
}