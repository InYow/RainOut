using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameObjPool : MonoBehaviour
{
    //TIP 生成对象后，要紧接着SetPrefab
    //FIN 管理
    //FIN 检索，动态生成
    List<GameObject> _list;
    GameObject _prefab;
    public void SetPrefab(GameObject prefab)
    {
        _list = new();
        _prefab = prefab;
    }
    public GameObject Get()
    {
        if (_prefab == null)
        {
            Debug.Log($"没有为{gameObject}缓冲池赋值SetPrefab");
        }
        if (_list.Count == 0)
        {
            GameObject ob1 = Instantiate(_prefab);
            _list.Add(ob1);
            return ob1;
        }
        foreach (var ob2 in _list)
        {
            if (!ob2.activeSelf)
                return ob2;
        }
        GameObject ob3 = Instantiate(_prefab);
        _list.Add(ob3);
        return ob3;
    }
}
