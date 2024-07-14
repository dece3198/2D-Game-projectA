using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelect : MonoBehaviour
{
    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos = new Vector2(Mathf.Round(mPos.x), Mathf.Round(mPos.y));
        transform.position = mPos;
    }
}
