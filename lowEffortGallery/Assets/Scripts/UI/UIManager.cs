using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject CursorPanel;
    public GameObject CoinsPanel;
    public GameObject ControlsPanel;
    public GameObject DefaultAvatarPanel;
    public GameObject FlyCamAvatarPanel;
    public bool isMenu = false;
    
    public float time = 1f;
    //public Vector2 coinsAnchorPos = new Vector2(-840, -470);
    public float anchorScale = 1f;
    public Vector3 anchorRotation = Vector3.zero;
    public RectTransform coinsAnchorGame;
    public RectTransform coinsAnchorMenu;

    public ChooseCharacter chooseCharacters;

    public GameObject exitMenu;
    public GameObject CreditsPanel;
    public TextMeshProUGUI NewGameText;
    


    private void Start()
    {
        if (isMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CursorPanel.SetActive(false);

            NewGamesCounter();
        }
        else
        {
            //запуск игры не в сцене меню
            GameManager.Instance.ChangeAvatar(0f);
            GameManager.Instance.ChangeController(GameManager.Instance.isFlyCam);
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

    public void NewGame()
    {
        ResumeGame();
        SetupControls(); 
        GameManager.Instance.ChangeController(GameManager.Instance.isFlyCam);
        isMenu = false;
        CoinsToAnchor(coinsAnchorGame.position);
        GameManager.Instance.ChangeAvatar(2f);
        EventManager.OnNewGame.Invoke();
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        isMenu = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CursorPanel.SetActive(false);
        CoinsPanel.SetActive(true);
        CoinsToAnchor(coinsAnchorMenu.position);
        EventManager.OnResumeGame.Invoke();
        PausePannel.SetActive(false);
        GameManager.Instance.ExitToMenu();
        
        if(chooseCharacters != null)
            chooseCharacters.ActivateCharacters();
    }

    public void  CoinsToAnchor(Vector3 pos)
    {
        RectTransform coinsRectTransform = CoinsPanel.transform.GetChild(0).GetComponent<RectTransform>();
        if (coinsRectTransform != null)
        {
            coinsRectTransform.DOMove(pos, time).SetEase(Ease.Flash);
            coinsRectTransform.DOScale(anchorScale, time);
            coinsRectTransform.DORotate(anchorRotation, time);
        }
            
    }

    private void NewGamesCounter()
    {
        var runs = GameVariables.instance.RunsCompleted;
        NewGameText.fontSize = 40 + 30 * runs;
        string pluses = "";
        
        for (int i = 0; i < runs; i++)
        {
            pluses += "+ ";
        }

        NewGameText.text = "New Game " + pluses;
    }

    public void AreYouSure()
    {
        if (exitMenu.activeSelf)
        {
            exitMenu.SetActive(false);
        }
        else
        {
            exitMenu.SetActive(true);
        }
    }

    public void ExitNo()
    {
        exitMenu.SetActive(false);
    }

    public void Credits()
    {
        if (CreditsPanel.activeSelf)
        {
            CreditsPanel.SetActive(false);
        }
        else
        {
            CreditsPanel.SetActive(true);
        }
    }
    public void QuitGame(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }
}
