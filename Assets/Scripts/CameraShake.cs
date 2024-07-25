using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    Vector3 originPos;

    public void Shake()
    {
        StartCoroutine(ShakeCo(duration, magnitude));
    }

    private IEnumerator ShakeCo(float duration, float magnitude)
    {
        float timer = 0;
        originPos = transform.position;

        while(timer < duration)
        {
            transform.localPosition = Random.insideUnitSphere * magnitude + originPos;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originPos;
    }
}
