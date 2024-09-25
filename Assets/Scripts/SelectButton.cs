using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image curImage;
    [SerializeField] private InputField inputField;
    public GameObject questionImage;
    public TextMeshProUGUI saveText;
    public bool isSave = false;
    public PlayerData curDate;

    private void Awake()
    {
        curImage = GetComponent<Image>();
    }

    private void Update()
    {
        if(inputField.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(inputField.text.Length > 0)
                {
                    DataManager.instance.curData.saveName = saveText.text;
                    curDate.saveName = saveText.text;
                    saveText.text = inputField.text;
                    curDate.name = saveText.text;
                    DataManager.instance.curData.name = saveText.text;
                    inputField.gameObject.SetActive(false);
                    isSave = true;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        curImage.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        curImage.color = Color.white;
        inputField.gameObject.SetActive(false);
    }

    public void SaveButton()
    {
        if (isSave)
        {
            DataManager.instance.curData = curDate;
            SceneManager.LoadScene("2D Game");
            return;
        }
        inputField.gameObject.SetActive(true);
    }

    public void DeleteButton()
    {
        if(curDate.saveName.Length > 0)
        {
            SaveButtonManager.instance.curButton = this;
            questionImage.SetActive(true);
        }
    }
}
