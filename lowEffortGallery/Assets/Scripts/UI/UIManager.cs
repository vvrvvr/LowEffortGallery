using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject CursorPanel;
    


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.isPause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        EventManager.OnPause.Invoke();
        
        PausePannel.SetActive(true);
        GameManager.Instance.PauseGame();
        
        CursorPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        EventManager.OnResumeGame.Invoke();
        
        PausePannel.SetActive(false);
        GameManager.Instance.ResumeGame();
        
        CursorPanel.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
