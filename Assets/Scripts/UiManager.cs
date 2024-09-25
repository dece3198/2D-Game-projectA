using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject information;
    [SerializeField] private GameObject inventory;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemdata;
    [SerializeField] private GameObject escImage;
    private bool inventoryActiveSelf = false;
    private bool isEsc = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActiveSelf = !inventoryActiveSelf;
            if(inventoryActiveSelf)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
                information.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isEsc = !isEsc;
            if(isEsc)
            {
                Time.timeScale = 0;
                escImage.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                escImage.SetActive(false);
            }
        }

    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        isEsc = false;
        escImage.SetActive(false);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Information(Item item)
    {
        itemName.text = item.information.name;
        itemType.text = item.information.type;
        itemPrice.text = item.price.ToString();
        itemdata.text = item.information.data;
    }
}
