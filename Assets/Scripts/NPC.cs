using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Talk talk;

    private void OnMouseOver()
    {
        GameManager.instance.MouseCursorChangeTalk();
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {
        TalkManager.instance.ChangeTalk(this,talk);
    }
}
