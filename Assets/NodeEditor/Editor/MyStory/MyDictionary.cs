using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MyDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    public new TValue this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public new void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
        RefreshLists();
    }

    public new bool Remove(TKey key)
    {
        if (dictionary.Remove(key))
        {
            RefreshLists();
            return true;
        }
        return false;
    }

    public new bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }

    public new bool TryGetValue(TKey key, out TValue value)
    {
        return dictionary.TryGetValue(key, out value);
    }

    public new IEnumerable<TKey> Keys => dictionary.Keys;

    public new IEnumerable<TValue> Values => dictionary.Values;

    public void OnBeforeSerialize()
    {
        RefreshLists();
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }
    }

    private void RefreshLists()
    {
        keys.Clear();
        values.Clear();
        foreach (var pair in dictionary)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
}
