using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject crops;
    private bool isMouse = true;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseOver()
    {
        if(isMouse)
        {
            MouseSelect.instance.isSelect = false;
            GameManager.instance.MouseCursorChangePlus();
            isMouse = false;
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        MouseSelect.instance.isSelect = true;
        isMouse = true;
    }

    public void OnMouseDown()
    {
        GameObject _crops = Instantiate(crops);
        _crops.transform.position = transform.position;
        _crops.GetComponent<Rigidbody2D>().gravityScale = 1;
        _crops.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        StartCoroutine(TileManager.instance.GravityCo(_crops));
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        SetColor(0);
        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        MouseSelect.instance.isSelect = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void SetColor(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
