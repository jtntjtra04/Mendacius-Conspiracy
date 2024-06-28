using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D default_cursor;
    public Texture2D hover_cursor;

    private void Start()
    {
        Cursor.SetCursor(default_cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
