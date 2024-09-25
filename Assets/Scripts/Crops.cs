using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject crops;
    public bool isMouse = true;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Seed seed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        seed = GetComponent<Seed>();
    }

    public void Update()
    {
        
    }

    public void OnMouseOver()
    {
        if(isMouse)
        {
            if (MouseSelect.instance != null)
            {
                MouseSelect.instance.isSelect = false;
            }
            GameManager.instance.MouseCursorChangePlus();
            isMouse = false;
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        if (MouseSelect.instance != null)
        {
            MouseSelect.instance.isSelect = true;
        }
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


        for(int i = 0; i < CropsManager.instance.Seeds.Count; i++)
        {
            if(seed == CropsManager.instance.Seeds[i])
            {
                CropsManager.instance.Seeds.RemoveAt(i);
            }
        }
        SetColor(0);
        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        if (MouseSelect.instance != null)
        {
            MouseSelect.instance.isSelect = true;
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void SetColor(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
