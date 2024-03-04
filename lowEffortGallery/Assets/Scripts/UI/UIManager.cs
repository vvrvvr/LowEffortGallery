using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public GameObject firstTimeCameraBoughtPanel;
    
    


    private void Start()
    {
        if (isMenu)
        {
            firstTimeCameraBoughtPanel.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CursorPanel.SetActive(false);
            if (GameVariables.instance.isCameraBought && GameVariables.instance.cameraBoughtFirstTimeMenu)
            {
                GameVariables.instance.cameraBoughtFirstTimeMenu = false;
                GameManager.Instance.isFlyCam = true;
                GameVariables.instance.isFlyCam = true;
                firstTimeCameraBoughtPanel.SetActive(true);
                Debug.Log("Camera bought first time show menu and logic");
            }
            
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

        if (isMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            AreYouSure();
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
        firstTimeCameraBoughtPanel.SetActive(false);
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
    
   
    public void LoadSceneByNameAsync(string sceneName)
    {
        GameManager.Instance.FadeIn(true);
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не будет полностью загружена
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // progress становится 1, когда загрузка завершена
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
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
