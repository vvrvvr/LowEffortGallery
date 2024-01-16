using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private GameObject defaultController;
    [SerializeField] private GameObject flyCamController;
    public CinemachineVirtualCamera DefaultCamera;
    public CinemachineVirtualCamera FlyCamera;
    public GameObject Cursor;
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    
    [HideInInspector] public bool isFlyCam = false;
    [HideInInspector] public int coins = 0;
    [HideInInspector] public Texture2D[] texturesArray= new Texture2D[3];
    [HideInInspector] public Texture2D[] texturesArrayTest = new Texture2D[3]; //delete after 
    
    public string folderName = "galleryFiles";
    public string fileNamePrefix = "Screenshot";

    private int screenshotCount = 0;
    private CinemachineImpulseSource impulseSource;
    public float impulsePower;
    

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
        texturesArray = GameVariables.instance.texturesArray;
        texturesArrayTest = GameVariables.instance.texturesArrayTest;
    }

    private void Start()
    {
        ChangeController(isFlyCam);
        fadeImage.gameObject.SetActive(true);
        FadeOut();
        impulseSource = GetComponent<CinemachineImpulseSource>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
           
               // ApplySavedTexture();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("saved");
           // SaveTextureToFile();
        }
    }

    // private int score = 0;
    //
    // public void IncreaseScore()
    // {
    //     score++;
    //     Debug.Log("Score: " + score);
    // }

    private void ChangeController(bool isFly)
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
        coins++;
        GameVariables.instance.coins = coins;
    }
    
    public void FadeIn()
    {
        fadeImage.DOFade(1.0f, fadeSpeed).OnComplete(AfterFade);
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

    public void LoadNextLevel()
    {
        FadeIn();
        DOTween.Sequence()
            .AppendInterval(fadeSpeed)  
            .OnComplete(afterWait);
    }
    void afterWait()
    {
        Debug.Log("After waiting!");
    }


    public void SavePhotoTextureToArray(Texture2D textureToSave)
    {
        texturesArray[screenshotCount] = textureToSave; //потом удалить чтобы хранилось только в переменных
        GameVariables.instance.texturesArray[screenshotCount] = textureToSave;
        screenshotCount++;
        if (screenshotCount >= texturesArray.Length)
        {
            screenshotCount = 0;
        }
    }
    // private void ApplySavedTexture(Texture2D choosenTexture, GameObject photoObject)
    // {
    //     // Apply the saved texture to the photoObject
    //     Renderer renderer = photoObject.GetComponent<Renderer>();
    //     if (renderer != null)
    //     {
    //         renderer.material.mainTexture = choosenTexture;
    //         Debug.Log("Texture applied to photoObject");
    //     }
    //     else
    //     {
    //         Debug.LogError("photoObject does not have a Renderer component");
    //     }
    // }

    public void SaveTextureToFile(Texture2D choosenTexture)
    {
        // Create the folder if it does not exist
        string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // Save texture to file
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName, fileNamePrefix + screenshotCount.ToString() + ".png");
        byte[] bytes = choosenTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    public void PhotoFlash()
    {
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
            coins -= obj.cost;
            GameVariables.instance.coins = coins;
            obj.isBought = true;
            obj.InterfaceBought();
        }
        else
        {
            impulseSource.GenerateImpulse(impulsePower);
            Debug.Log("cant buy");
        }
    }

    public void FlyCameraBought()
    {
        Debug.Log("cam boought");
    }
}