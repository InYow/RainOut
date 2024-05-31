using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectTileBox : MonoBehaviour
{
    public Color availableColor;
    public Color disableColor;
    public SpriteRenderer spriteRenderer;
    public Tilemap tilemap;
    public TilemapGameobject tilemapGameobject;
    public Vector3 Anchor;
    private void Start()
    {
        GameObject gridObject = GameObject.FindWithTag("HoeLayer");
        if (gridObject != null)
        {
            tilemap = gridObject.GetComponent<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError("Tilemap component not found on the game object.");
            }
            tilemapGameobject = gridObject.GetComponent<TilemapGameobject>();
            if (tilemapGameobject == null)
            {
                Debug.LogError("TilemapGameobject component not found on the game object.");
            }
        }
        else
        {
            Debug.LogError("Game object with tag 'HoeLayer' not found.");
        }

    }
    void Update()
    {
        //mouse-position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //tile-position
        Vector3Int vector3int = tilemap.WorldToCell(mouseWorldPos);
        //tileObject
        GameObject tileObject = tilemapGameobject.GetTileGameObject(vector3int);
        //HoeDirt
        HoeDirt hoeDirt = null;
        if (tileObject != null)
        {
            hoeDirt = tileObject.GetComponent<HoeDirt>();
        }
        // && tile.gameObject.GetComponent<HoeDirt>().plant == null
        if (tileObject != null && hoeDirt != null && hoeDirt.plant == null)
        {
            Debug.Log($"{vector3int} {tileObject.name}");
            // 能种下去
            spriteRenderer.color = availableColor;
        }
        else
        {
            spriteRenderer.color = disableColor;
        }
        //this-position
        Vector3 vector3 = vector3int + Anchor;
        transform.position = vector3;
    }
}
