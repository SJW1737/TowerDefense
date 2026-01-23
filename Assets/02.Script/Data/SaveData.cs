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
    public bool startGoldApplied;

    public int diamond;

    public List<RelicSaveData> relics = new();
}
