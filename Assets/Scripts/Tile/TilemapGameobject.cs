using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//添加到要存储的Tilemap.gameObject上
public class TilemapGameobject : MonoBehaviour
{
    // 存储Tile位置与GameObject的关联
    private Dictionary<Vector3Int, GameObject> tileGameObjectMap = new Dictionary<Vector3Int, GameObject>();

    /// <summary>
    /// 获取指定位置的Tile的GameObject
    /// </summary>
    /// <param name="position">Tile的位置</param>
    /// <returns>Tile关联的GameObject</returns>
    public GameObject GetTileGameObject(Vector3Int position)
    {
        tileGameObjectMap.TryGetValue(position, out GameObject tileObject);
        return tileObject;
    }

    /// <summary>
    /// 获取指定GameObject的Tile位置
    /// </summary>
    /// <param name="gameObject">需要查找的GameObject</param>
    /// <returns>GameObject关联的Tile位置</returns>
    public Vector3Int? GetTilePosition(GameObject gameObject)
    {
        foreach (var kvp in tileGameObjectMap)
        {
            if (kvp.Value == gameObject)
            {
                return kvp.Key;
            }
        }
        return null;
    }

    /// <summary>
    /// 添加Tile位置与GameObject的映射
    /// </summary>
    /// <param name="position">Tile的位置</param>
    /// <param name="gameObject">关联的GameObject</param>
    public void AddTileGameObject(Vector3Int position, GameObject gameObject)
    {
        tileGameObjectMap[position] = gameObject;
    }

    /// <summary>
    /// 移除Tile位置与GameObject的映射
    /// </summary>
    /// <param name="position">Tile的位置</param>
    public void RemoveTileGameObject(Vector3Int position)
    {
        tileGameObjectMap.Remove(position);
    }
}

