using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ProgresSO")]
public class ProgresSO : ScriptableObject
{
    public List<Condition> conditions = new List<Condition>();
}

