using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject sleep;
    [SerializeField] private TextMeshProUGUI sleepText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.instance.isNight || GameManager.instance.PlayerStamina <= 10)
        {
            sleep.SetActive(true);
            sleepText.text = "���� ���� �߱��?";
        }
        else
        {
            sleep.SetActive(true);
            sleepText.text = "���� �� �� ���� �ð��Դϴ�.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sleep.SetActive(false);
    }

    public void YesButton()
    {
        if (GameManager.instance.isNight || GameManager.instance.PlayerStamina <= 10)
        {
            GameManager.instance.Sleep();
            FadeInOut.instance.Fade();
            sleep.SetActive(false);
        }
        else
        {
            sleep.SetActive(false);
        }
    }

    public void NoButton()
    {
        sleep.SetActive(false);
    }
}
