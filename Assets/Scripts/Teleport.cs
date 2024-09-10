using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform MovePos;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject nextWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            FadeInOut.instance.Fade();
            if(wall != null)
            {
                wall.SetActive(false);
            }
            if(nextWall != null)
            {
                nextWall.SetActive(true);
            }
            StartCoroutine(TelePortCo(collision));
        }
    }

    private IEnumerator TelePortCo(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.transform.position = MovePos.position;
    }
}
