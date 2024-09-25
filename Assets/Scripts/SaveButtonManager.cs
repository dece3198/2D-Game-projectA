using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonManager : MonoBehaviour
{
    public static SaveButtonManager instance;
    [SerializeField] private SelectButton[] selectButtons;
    public SelectButton curButton;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < selectButtons.Length; i++)
        {
            DataManager.instance.LoadData(selectButtons[i].saveText.text);
            if(DataManager.instance.curData.saveName == selectButtons[i].saveText.text)
            {
                selectButtons[i].saveText.text = DataManager.instance.curData.name + " " + DataManager.instance.curData.month.ToString() + "¿ù";
                selectButtons[i].isSave = true;
                selectButtons[i].curDate = DataManager.instance.curData;
                DataManager.instance.curData.isStart = false;
            }
        }
    }

    public void DeleteButton()
    {
        curButton.saveText.text = curButton.curDate.saveName;
        DataManager.instance.DeleteData(curButton.curDate.saveName);
        curButton.curDate = new PlayerData();
        curButton.isSave = false;
        curButton.questionImage.SetActive(false);
    }

    public void NoButton()
    {
        curButton.questionImage.SetActive(false);
    }
}
