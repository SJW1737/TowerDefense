using System;
using System.Collections.Generic;

[Serializable]
public class RelicSaveData
{
    public string relicId;
    public int level;
}

[Serializable]
public class SaveData
{
    public List<RelicSaveData> relics = new();
}
