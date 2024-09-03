using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Texture2D cursorImageA;
    [SerializeField] private Texture2D cursorImageB;

    public float gold;

    private float minute;
    public float Minute
    {
        get { return minute; }
        set 
        {
            minute = value; 
            if(minute >= 60)
            {
                Hour += 1;
                minute = 0;
            }
        }
    }

    private int hour;
    public int Hour
    {
        get { return hour; }
        set 
        { 
            hour = value; 

            if(hour > 12)
            {
                hour = 1;
                isAfternoon = true;
            }
            else if(hour > 11)
            {
                amPm = "오후";
            }

            if(hour > 5 && isAfternoon)
            {
                isNight = true;
            }
        }
    }

    [SerializeField]private float playerStamina;
    public float PlayerStamina
    {
        get { return playerStamina; }
        set 
        { 
            playerStamina = value; 
        }
    }

    public float maxStamina;
    public float nightCount = 0;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image night;
    private string amPm = "오전";
    public bool isNight = false;
    private bool isAfternoon = false;

    private void Awake()
    {
        instance = this;
        hour = 6;
        timeText.text = string.Format("{0:D2}:{1:D2}", hour, (int)Minute) + " " + amPm;
        gold = 500;
    }

    private void Start()
    {
        StartCoroutine(TimeCo());
    }

    private void Update()
    {
        staminaSlider.value = playerStamina / maxStamina;
        staminaText.text = playerStamina.ToString() + "/" + maxStamina.ToString();
        goldText.text = gold.ToString();
        if(playerStamina >= maxStamina)
        {
            staminaSlider.gameObject.SetActive(false);
        }
        else
        {
            if (!staminaSlider.gameObject.activeSelf)
            {
                staminaSlider.gameObject.SetActive(true);
            }
        }
    }

    public void MouseCursorChangeTalk()
    {
        Cursor.SetCursor(cursorImageA, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void MouseCursorChangePlus()
    {
        Cursor.SetCursor(cursorImageB, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Sleep()
    {
        amPm = "오전";
        hour = 6;
        CropsManager.instance.ResetSeed();
        isNight = false;
        isAfternoon = false;
        nightCount = 0;
    }

    private void SetColor(float alpha)
    {
        Color color = night.color;
        color.a = alpha;
        night.color = color;
    }

    private IEnumerator TimeCo()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            Minute += 10;
            if(isNight)
            {
                nightCount += 0.05f;
                if(nightCount >= 0.5f)
                {
                    nightCount = 0.5f;
                }
                SetColor(nightCount);
            }
            timeText.text = string.Format("{0:D2}:{1:D2}", hour, (int)Minute) + " " + amPm;
        }
    }
}
