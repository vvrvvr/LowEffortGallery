using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private GameObject defaultController;
    [SerializeField] private GameObject flyCamController;
    public CinemachineVirtualCamera DefaultCamera;
    public CinemachineVirtualCamera FlyCamera;
    public CinemachineVirtualCamera MenuCamera;
    public GameObject Cursor;
    public Image fadeImage;
    public float fadeSpeed = 2.0f;
    
    [HideInInspector] public bool isFlyCam = false;
    [HideInInspector] public int coins = 0;
    //public Texture2D[] texturesArray= new Texture2D[3];
    public TextureData[] texturesArray = new TextureData[3];
    //[HideInInspector] public Texture2D[] texturesArrayTest = new Texture2D[3]; //delete after
    public FollowAnchor _Avatars;
    
    public string folderName = "NOT GALLERY 2 PHOTOS";
    public string fileNamePrefix = "ingame photo ";

    private int screenshotCount = 0;
    private CinemachineImpulseSource impulseSource;
    public float impulsePower;
    
    public TextMeshProUGUI CoinsText;
    public bool isPause = false;
    public UIManager _UIManager;
    [Space(20)]
    public AudioSource _Audio;

    public AudioClip bougth;
    public AudioClip boughtError;
    public AudioClip savedToDesctop;
    public AudioClip coinSound;
    public SoundManager[] _soundManager = new SoundManager[1];

    public float photoFlashVolume = 1f;
    public AudioClip photoFlashSound;
    

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    var singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }

            //DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isFlyCam = GameVariables.instance.isFlyCam;
        coins = GameVariables.instance.coins;
        UpdateCoins(coins);
        texturesArray = GameVariables.instance.textureDataArray;
        //texturesArrayTest = GameVariables.instance.texturesArrayTest;
    }

    private void Start()
    {
       // ChangeController(isFlyCam);
        fadeImage.gameObject.SetActive(true);
        FadeOut();
        impulseSource = GetComponent<CinemachineImpulseSource>();

    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.O))
    //     {
    //        
    //            // ApplySavedTexture();
    //     }
    //     if (Input.GetKeyDown(KeyCode.I))
    //     {
    //         Debug.Log("saved");
    //        //SaveTextureToFile(1);
    //     }
    // }

    public void PauseGame()
    {
        isPause = true;
    }

    public void ResumeGame()
    {
        isPause = false;
    }

    public void ExitToMenu()
    {
        ResumeGame();
        _Avatars.HideAvatars();
        DeactivateControllersToMenu();
    }

    public void DeactivateControllersToMenu()
    {
        DefaultCamera.Priority = 0;
        FlyCamera.Priority = 0;
        MenuCamera.Priority = 2;
        flyCamController.SetActive(false);
        defaultController.SetActive(false);
    }

    public void ChangeController(bool isFly)
    {
        if (isFly)
        {
            DefaultCamera.Priority = 0;
            FlyCamera.Priority = 10;
            flyCamController.SetActive(true);
            defaultController.SetActive(false);
            
            if(Cursor != null)
                Cursor.SetActive(false);
        }
        else
        {
            FlyCamera.Priority = 0;
            DefaultCamera.Priority = 10;
            flyCamController.SetActive(false);
            defaultController.SetActive(true);
            
            if(Cursor != null)
                Cursor.SetActive(true);
        }
    }

    public void IncreaseCoins()
    {
        _Audio.clip = coinSound;
        _Audio.Play();
        coins++;
        GameVariables.instance.coins = coins;
        UpdateCoins(coins);
    }
    
    public void FadeIn()
    {
        fadeImage.DOFade(1.0f, fadeSpeed).OnComplete(AfterFade);
    }
    public void FadeIn(bool b)
    {
        fadeImage.DOFade(1.0f, fadeSpeed);
    }
    
    public void FadeOut()
    {
        fadeImage.DOFade(0.0f, fadeSpeed).OnComplete(AfterFade);
    }
    
    private void AfterFade()
    {
        Debug.Log("Fade completed! Do something here.");
        // Дополнительные действия после завершения анимации
    }
    
    public void LoadNextLevel(string levelName)
    {
        Debug.Log("exit");
        if (_soundManager != null)
        {
            for (int i = 0; i < _soundManager.Length; i++)
            {
                
                _soundManager[i].FadeSound(fadeSpeed-0.5f, false);
            }
           
        }
        FadeIn();
        DOTween.Sequence()
            .AppendInterval(fadeSpeed)  
            .OnComplete(() => afterWait(levelName));
    }
    void afterWait(string level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("After waiting!");
    }

    private void UpdateCoins(int coins)
    {
        if(CoinsText!=null)
            CoinsText.text = "coins: " + coins;
    }


    // public void SavePhotoTextureToArray(Texture2D textureToSave)
    // {
    //     //texturesArray[screenshotCount] = textureToSave; //потом удалить чтобы хранилось только в переменных
    //     GameVariables.instance.texturesArray[screenshotCount] = textureToSave;
    //     screenshotCount++;
    //     if (screenshotCount >= texturesArray.Length)
    //     {
    //         screenshotCount = 0;
    //     }
    // }

    public void SaveTextureToFile(int photosArrayId)
    {
        
        // Create the folder if it does not exist
        string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // Save texture to file
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName, fileNamePrefix + screenshotCount.ToString() + ".png");
        screenshotCount++;
        byte[] bytes = texturesArray[photosArrayId].texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    public void PhotoFlash()
    {
        Debug.Log("photo!");
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImage.DOColor(Color.white, 0f));
        sequence.Append(fadeImage.DOFade(1f, 0.2f));
        sequence.Append(fadeImage.DOFade(0f, 0.4f));
        sequence.Append(fadeImage.DOColor(Color.black, 0f));
        sequence.Append(fadeImage.DOFade(0f, 0f));
    }

    public void BuyObject(InterfaceObject obj)
    {
        if (coins >= obj.cost)
        {
            _Audio.clip = bougth;
            _Audio.Play();
            
            coins -= obj.cost;
            GameVariables.instance.coins = coins;
            UpdateCoins(coins);
            obj.isBought = true;
            obj.InterfaceBought();
            if (obj.isCam)
            {
                DialogueManager.instance.FrogSay("cameraBought");
                GameVariables.instance.cameraBoughtFirstTimeMenu = true;
            }
            else
            {
                DialogueManager.instance.FrogSay("photoBought");
            }
        }
        else
        {
            impulseSource.GenerateImpulse(impulsePower);
            
            _Audio.clip = boughtError;
            _Audio.Play();
            
            if (obj.isCam) //не удалось купить камеру
            {
                DialogueManager.instance.FrogSay("cantBuyCam");
            }
            else
            {
                DialogueManager.instance.FrogSay("cantBuyPhoto");
            }
            
            Debug.Log("cant buy");
        }
    }

    public void FlyCameraBought()
    {
        Debug.Log("cam boought");
        GameVariables.instance.isCameraBought = true;
        if(_UIManager !=null)
            _UIManager.SetupControls();
    }

    public void ChangeIsFly()
    {
        isFlyCam = !isFlyCam;
        GameVariables.instance.isFlyCam = isFlyCam;
        ChangeController(isFlyCam);
    }
    
    public void ChangeAvatar(float delay)
    {
        if(_Avatars != null)
            _Avatars.SetupAvatars(GameVariables.instance.AvatarID, delay);
    }
}