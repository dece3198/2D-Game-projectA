using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject saveMenu;

    private void Update()
    {
        if(saveMenu.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                saveMenu.SetActive(false);
            }
        }
    }

    public void StartButton()
    {
        saveMenu.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
