using System;
using System.Collections.Generic;

[Serializable]
public class RelicSaveData
{
    public string relicId;
    public int level;
    public int piece;
}

[Serializable]
public class SaveData
{
    public bool startGoldApplied;

    public int diamond;

    public List<RelicSaveData> relics = new();
}
