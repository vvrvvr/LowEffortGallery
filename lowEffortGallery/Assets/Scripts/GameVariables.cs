using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables instance;
    
    public bool isFlyCam = false;
    public int coins = 0;
    public bool isCameraBought = false;
    public Texture2D[] texturesArray= new Texture2D[3];
    public Texture2D[] texturesArrayTest = new Texture2D[3]; //delete after 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}