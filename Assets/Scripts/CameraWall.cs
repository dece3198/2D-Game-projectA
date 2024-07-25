using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWall : MonoBehaviour
{
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        float Ix = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(Camera.main.transform.position.x, -Ix + center.x, Ix + center.x);

        float Iy = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(Camera.main.transform.position.y, -Iy + center.y, Iy + center.y);

        Camera.main.transform.position = new Vector3(clampX, clampY, -10f);
    }
}
