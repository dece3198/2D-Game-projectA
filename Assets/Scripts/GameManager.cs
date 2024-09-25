using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DayType
{
    월, 화, 수, 목, 금, 토, 일
}


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
                dayImage.sprite = moonImage;
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
            if(playerStamina > 100)
            {
                playerStamina = 100;
            }
        }
    }

    public float maxStamina;
    public float nightCount = 0;

    [SerializeField] private int day;
    public int Day
    {
        get { return day; }
        set 
        { 
            day = value; 

            if(Month == 2)
            {
                if(day > 28)
                {
                    Month++;
                    day = 1;
                }
            }
            else if(Month == 4 || Month == 6 || Month == 9 || Month == 11)
            {
                if(day > 30)
                {
                    Month++;
                    day = 1;
                }
            }
            else
            {
                if(day > 31)
                {
                    Month++;
                    day = 1;
                }
            }
        }
    }

    [SerializeField] private int month;
    public int Month
    {
        get { return month; }
        set 
        { 
            month = value;

            monthText.text = month.ToString() + "월";

            if(month > 12)
            {
                month = 1;
            }

            if(month > 2 && month <= 6)
            {
                season.sprite = springImage;
            }
            else if(month > 5 && month < 9)
            {
                season.sprite = summerImage;
            }
            else if(month > 8 && month < 12)
            {
                season.sprite = autumnImage;
            }
            else
            {
                season.sprite = winterImage;
            }
        }
    }

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image night;
    [SerializeField] private Image dayImage;
    [SerializeField] private Sprite sunImage;
    [SerializeField] private Sprite rainImage;
    [SerializeField] private Sprite moonImage;
    [SerializeField] private Image season;
    [SerializeField] private Sprite springImage;
    [SerializeField] private Sprite summerImage;
    [SerializeField] private Sprite autumnImage;
    [SerializeField] private Sprite winterImage;

    private string amPm = "오전";
    public bool isNight = false;
    private bool isAfternoon = false;
    public DayType curDay;
    private Dictionary<DayType, string> dayDic = new Dictionary<DayType, string>();
    private Dictionary<string, DayType> dayStringDic = new Dictionary<string, DayType>();

    private void Awake()
    {
        instance = this;
        timeText.text = string.Format("{0:D2}:{1:D2}", hour, (int)Minute) + " " + amPm;

        dayDic.Add(DayType.월, "월");
        dayDic.Add(DayType.화, "화");
        dayDic.Add(DayType.수, "수");
        dayDic.Add(DayType.목, "목");
        dayDic.Add(DayType.금, "금");
        dayDic.Add(DayType.토, "토");
        dayDic.Add(DayType.일, "일");
        dayStringDic.Add("월", DayType.월);
        dayStringDic.Add("화", DayType.화);
        dayStringDic.Add("수", DayType.수);
        dayStringDic.Add("목", DayType.목);
        dayStringDic.Add("금", DayType.금);
        dayStringDic.Add("토", DayType.토);
        dayStringDic.Add("일", DayType.일);
    }

    private void Start()
    {
        StartCoroutine(TimeCo());
    }

    private void Update()
    {
        goldText.text = gold.ToString() + "G";
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Sleep();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            Month += 1;
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
        Minute = 0;
        CropsManager.instance.ResetSeed();
        isNight = false;
        isAfternoon = false;
        nightCount = 0;
        Day++;
        dayImage.sprite = sunImage;
        NextDay();
        TalkManager.instance.NextDay();
        playerStamina += 50;
        dayText.text = dayDic[curDay] + "," + Day.ToString();
        timeText.text = string.Format("{0:D2}:{1:D2}", hour, (int)Minute) + " " + amPm;
        staminaText.text = playerStamina.ToString() + "/" + maxStamina.ToString();
        staminaSlider.value = playerStamina / maxStamina;
        SaveData();
        DataManager.instance.SaveData();
    }

    public void LoadData()
    {
        gold = DataManager.instance.curData.gold;
        Minute = DataManager.instance.curData.minute;
        Hour = DataManager.instance.curData.hour;
        Day = DataManager.instance.curData.day;
        Month = DataManager.instance.curData.month;
        PlayerStamina = DataManager.instance.curData.playerStamina;

        if(DataManager.instance.curData.dayString.Length > 0)
        {
            curDay = dayStringDic[DataManager.instance.curData.dayString];
        }

        if (!DataManager.instance.curData.isStart)
        {
            
            TileManager.instance.gameObject.transform.position = DataManager.instance.curData.playerPos;
            for (int i = 0; i < DataManager.instance.curData.slotNumber.Count; i++)
            {
                InventoryManager.instance.slots[DataManager.instance.curData.slotNumber[i]].AddItem(InventoryManager.instance.itemDic[DataManager.instance.curData.itemNumber[i]], DataManager.instance.curData.itemCount[i]);
            }

            if(DataManager.instance.curData.cropsNumber.Count > 0)
            {
                for (int j = 0; j < DataManager.instance.curData.cropsNumber.Count; j++)
                {
                    GameObject crops = Instantiate(CropsManager.instance.cropsDic[DataManager.instance.curData.cropsNumber[j]]);
                    crops.transform.position = DataManager.instance.curData.cropsPos[j];
                    CropsManager.instance.Seeds.Add(crops.GetComponent<Seed>());
                    crops.GetComponent<Seed>().CurCount = DataManager.instance.curData.seedCount[j];
                    crops.GetComponent<Seed>().life = DataManager.instance.curData.seedLife[j];
                }
            }
        }

        dayText.text = dayDic[curDay] + "," + Day.ToString();
        timeText.text = string.Format("{0:D2}:{1:D2}", hour, (int)Minute) + " " + amPm;
        staminaText.text = playerStamina.ToString() + "/" + maxStamina.ToString();
    }

    private void SaveData()
    {
        DataManager.instance.curData.gold = gold;
        DataManager.instance.curData.minute = Minute;
        DataManager.instance.curData.hour = Hour;
        DataManager.instance.curData.day = Day;
        DataManager.instance.curData.month = Month;
        DataManager.instance.curData.playerStamina = playerStamina;
        DataManager.instance.curData.playerPos = TileManager.instance.gameObject.transform.position;

        DataManager.instance.curData.slotNumber.Clear();
        DataManager.instance.curData.itemNumber.Clear();
        DataManager.instance.curData.itemCount.Clear();
        DataManager.instance.curData.cropsNumber.Clear();
        DataManager.instance.curData.seedCount.Clear();
        DataManager.instance.curData.seedLife.Clear();
        DataManager.instance.curData.cropsPos.Clear();

        for (int i = 0; i < InventoryManager.instance.slots.Length; i++)
        {
            if (InventoryManager.instance.slots[i].item != null)
            {
                DataManager.instance.curData.slotNumber.Add(i);
                DataManager.instance.curData.itemNumber.Add(InventoryManager.instance.itemNumberDic[InventoryManager.instance.slots[i].item]);
                DataManager.instance.curData.itemCount.Add(InventoryManager.instance.slots[i].itemCount);
            }
        }

        if(CropsManager.instance.Seeds.Count > 0)
        {
            for (int j = 0; j < CropsManager.instance.Seeds.Count; j++)
            {
                DataManager.instance.curData.cropsNumber.Add(CropsManager.instance.seedDic[CropsManager.instance.Seeds[j].seedType]);
                DataManager.instance.curData.seedCount.Add(CropsManager.instance.Seeds[j].CurCount);
                DataManager.instance.curData.seedLife.Add(CropsManager.instance.Seeds[j].life);
                DataManager.instance.curData.cropsPos.Add(CropsManager.instance.Seeds[j].transform.position);
            }
        }

        DataManager.instance.curData.dayString = dayDic[curDay];
    }

    private void NextDay()
    {
        if(curDay == DayType.일)
        {
            curDay = DayType.월;
            return;
        }
        curDay++;
    }

    public void Stamina()
    {
        staminaSlider.value = playerStamina / maxStamina;
        staminaText.text = playerStamina.ToString() + "/" + maxStamina.ToString();
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
