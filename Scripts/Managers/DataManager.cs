using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager : MonoBehaviour
{
    private static DataManager s_instance;
    public static DataManager Instance { get { return s_instance; } }
    public Dictionary<string, CharacterStat> StatDict { get; private set; } = new Dictionary<string, CharacterStat>();

    void Awake()
    {
        s_instance = this;
        StatDict = LoadJson<CharacterStatData, string, CharacterStat>("PlayerData").MakeDict();
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);

    }
    void Start()
    {

    }

    void Update()
    {

    }
}
