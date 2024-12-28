using Data;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Define;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    Dictionary<int, JTestData> TestDict = new Dictionary<int, JTestData>();

    public void Init()
    {
        TestDict = LoadJson<Data.JTestDataLoader, int, Data.JTestData>("TestData").MakeDict();
    }

    public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/JsonData/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
