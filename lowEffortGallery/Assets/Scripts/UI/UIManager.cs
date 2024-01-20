using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject CursorPanel;
    public GameObject CoinsPanel;
    public GameObject ControlsPanel;
    public GameObject DefaultAvatarPanel;
    public GameObject FlyCamAvatarPanel;
    public bool isMenu = false;
    


    private void Start()
    {
        if (isMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CursorPanel.SetActive(false);
        }
        else
        {
            //временно
            CoinsPanel.SetActive(false);
            ResumeGame();
            SetupControls(); 
        }
       
    }

    private void Update()
    {
        if (!isMenu && Input.GetKeyDown(KeyCode.Escape))
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

    public void PauseGame()
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

    public void ResumeGame()
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

    public void SetupControls()
    {
        if (GameManager.Instance.isFlyCam)
        {
            DefaultAvatarPanel.SetActive(false);
            FlyCamAvatarPanel.SetActive(true);
        }
        else
        {
            DefaultAvatarPanel.SetActive(true);
            FlyCamAvatarPanel.SetActive(false);
            ControlsPanel.SetActive(true);
        }
        if(GameVariables.instance.isCameraBought)
            ControlsPanel.SetActive(true);
        else
            ControlsPanel.SetActive(false);
    }

    public void SwitchControls()
    {
        GameManager.Instance.ChangeIsFly();
        SetupControls();
    }
}
