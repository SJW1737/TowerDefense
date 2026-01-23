using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private const string SAVE_KEY = "SAVE_DATA";

    public SaveData Data { get; private set; }

    public int Diamond => Data.diamond;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }
    protected override void Init()
    {
        Load();
    }

    public void AddDiamond(int amount)
    {
        Data.diamond += amount;
        Save();
    }

    public bool SpendDiamond(int amount)
    {
        if (Data.diamond < amount)
            return false;

        Data.diamond -= amount;
        Save();
        return true;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Data = new SaveData();
            Save();
            return;
        }

        Data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVE_KEY));

        if (Data == null)
        {
            Data = new SaveData();
        }
    }
}
