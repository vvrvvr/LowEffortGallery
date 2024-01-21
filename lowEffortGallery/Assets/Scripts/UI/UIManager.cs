using UnityEngine;
using DG.Tweening;

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
}
