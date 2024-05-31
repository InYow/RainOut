using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeFloor : MonoBehaviour
{
    private Tilemap m_tilemap;
    public TileBase m_ruleTile;
    public static HoeFloor Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_tilemap = GetComponent<Tilemap>();
        if (m_ruleTile == null)
            Debug.Log($"{gameObject.name}的m_ruleTile未设置！！！");
    }
    public void SetHoeDirt(Vector3Int vector3Int)
    {
        m_tilemap.SetTile(vector3Int, m_ruleTile);
    }
    public void RemoveHoeDirt(Vector3Int vector3Int)
    {
        m_tilemap.SetTile(vector3Int, null);
    }
}
