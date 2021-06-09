using System;

[Serializable]
public class Condition
{
    public string name;
    public bool isFulfil;

    public Condition(string name, bool isFulfil)
    {
        this.name = name;
        this.isFulfil = isFulfil;
    }
}
