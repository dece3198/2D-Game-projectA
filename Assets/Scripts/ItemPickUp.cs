using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(PickUpCo(collision));
        }
    }

    private IEnumerator PickUpCo(Collider2D collision)
    {
        float time = 0.25f;

        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, collision.transform.position, 0.1f);
            yield return null;
        }
        InventoryManager.instance.AcquireItem(item);
        Destroy(this.gameObject);
    }
}
