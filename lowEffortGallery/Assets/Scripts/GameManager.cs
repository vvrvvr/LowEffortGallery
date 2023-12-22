using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private GameObject defaultController;
    [SerializeField] private GameObject flyCamController;
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
}