using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    [HideInInspector]
    public static MouseCursor instance;
    void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        //TODO ：鼠标指针 Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
