using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveManager : MonoSingleton<SaveManager>
{
    private const string SAVE_KEY = "SAVE_DATA";

    public SaveData Data { get; private set; }

    public int Diamond => Data.diamond;

    public event Action<int> OnDiamondChanged;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }
    protected override void Init()
    {
        if (Data == null)
            Load();
    }

    public void AddDiamond(int amount)
    {
        Data.diamond += amount;
        Save();
        OnDiamondChanged?.Invoke(Data.diamond);
    }

    public bool SpendDiamond(int amount)
    {
        if (Data.diamond < amount)
            return false;

        Data.diamond -= amount;
        Save();
        OnDiamondChanged?.Invoke(Data.diamond);
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
