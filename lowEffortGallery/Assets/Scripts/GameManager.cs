using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private GameObject defaultController;
    [SerializeField] private GameObject flyCamController;
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    
    public bool isFlyCam = false;
    public int coins = 0;
    

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
}