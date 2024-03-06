using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables instance;
    
    public bool isFlyCam = false;
    public int coins = 0;
    public bool isCameraBought = false;
    public Texture2D[] texturesArray= new Texture2D[3];
    public int AvatarID = 0;
    public Texture2D[] texturesArrayTest = new Texture2D[3]; //delete after 
    public int RunsCompleted = 0;
    public bool cameraBoughtFirstTimeMenu = false;
    public int screenshotCount = 0;
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
    
    public void SavePhotoTextureToArray(Texture2D textureToSave)
    {
        texturesArray[screenshotCount] = textureToSave;
        screenshotCount++;
        if (screenshotCount >= texturesArray.Length)
        {
            screenshotCount = 0;
        }
    }
    
}