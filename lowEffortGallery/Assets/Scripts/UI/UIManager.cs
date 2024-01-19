using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject CursorPanel;
    public GameObject CoinsPanel;
    


    private void Start()
    {
        CoinsPanel.SetActive(false);
        //временно
        ResumeGame();
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
        if(CursorPanel != null)
            CursorPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(CoinsPanel != null)
            CoinsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        EventManager.OnResumeGame.Invoke();
        
        PausePannel.SetActive(false);
        GameManager.Instance.ResumeGame();

        if (GameManager.Instance.isFlyCam)
        {
            if(CoinsPanel != null)
                CoinsPanel.SetActive(false);
        }
        else
        {
            if(CursorPanel !=null)
                CursorPanel.SetActive(true);
            if(CoinsPanel != null)
                CoinsPanel.SetActive(true);
        }    
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
