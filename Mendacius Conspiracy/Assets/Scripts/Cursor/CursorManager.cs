using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D default_cursor;
    public Vector2 cursor_hotspot;
    public Texture2D hover_cursor;
    public Vector2 hover_cursor_hotspot;

    public void OnMouseEnter()
    {
        Cursor.SetCursor(hover_cursor, hover_cursor_hotspot, CursorMode.Auto);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(default_cursor, cursor_hotspot, CursorMode.Auto);
    }
}
