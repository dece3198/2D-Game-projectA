
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    [SerializeField] private Image panel;
    float time = 0;
    float F_time = 1;
    public bool isFade = false;

    private void Awake()
    {
        instance = this;
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    private IEnumerator FadeFlow()
    {
        isFade = false;
        panel.gameObject.SetActive(true);
        Color alpha = panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        time = 0;

        yield return new WaitForSeconds(1f);

        while (alpha.a > 0)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        isFade = true;
        panel.gameObject.SetActive(false);
    }
}
