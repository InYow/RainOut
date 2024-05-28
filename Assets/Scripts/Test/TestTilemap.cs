using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileBase;
    private void Start()
    {
        tilemap.SetTile(new(0, 0, 0), tileBase);
        Debug.Log("尝试绘制S");
    }
}
