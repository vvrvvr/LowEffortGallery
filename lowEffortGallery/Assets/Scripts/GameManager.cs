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
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    
    public bool isFlyCam = false;
    public int coins = 0;
    
    public string folderName = "galleryFiles";
    public string fileNamePrefix = "Screenshot";

    private int screenshotCount = 0;
    public Texture2D[] texturesArray= new Texture2D[3];
    public Texture2D[] texturesArrayTest = new Texture2D[3]; //delete after 
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

            DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    private int score = 0;

    public void IncreaseScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    private void ChangeController(bool isFly)
    {
        if (isFly)
        {
            flyCamController.SetActive(true);
            defaultController.SetActive(false);
        }
        else
        {
            flyCamController.SetActive(false);
            defaultController.SetActive(true);
        }
    }

    public void IncreaseCoins()
    {
        coins++;
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
        texturesArray[screenshotCount] = textureToSave;
        screenshotCount++;
        if (screenshotCount >= texturesArray.Length)
        {
            screenshotCount = 0;
        }
    }
    private void ApplySavedTexture(Texture2D choosenTexture, GameObject photoObject)
    {
        // Apply the saved texture to the photoObject
        Renderer renderer = photoObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = choosenTexture;
            Debug.Log("Texture applied to photoObject");
        }
        else
        {
            Debug.LogError("photoObject does not have a Renderer component");
        }
    }

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
            obj.isBought = true;
            obj.InterfaceBought();
        }
        else
        {
            impulseSource.GenerateImpulse(impulsePower);
            Debug.Log("cant buy");
        }
    }
}