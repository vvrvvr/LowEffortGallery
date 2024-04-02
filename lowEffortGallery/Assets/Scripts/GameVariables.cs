using UnityEngine;

[System.Serializable]
public struct TextureData
{
    public Texture2D texture;
    public int avatar;
    public int hall;
}

public class GameVariables : MonoBehaviour
{
    public static GameVariables instance;

    public bool isFlyCam = false;
    public int coins = 0;
    public bool isCameraBought = false;
    public TextureData[] textureDataArray = new TextureData[3]; // Массив структур TextureData
    public int AvatarID = 0;
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

    public void SavePhotoTextureToArray(Texture2D textureToSave, int avatar, int hall)
    {
        // Создание новой структуры TextureData и сохранение данных
        TextureData newData = new TextureData();
        newData.texture = textureToSave;
        newData.avatar = avatar;
        newData.hall = hall;

        // Сохранение структуры в массиве
        textureDataArray[screenshotCount] = newData;
        screenshotCount++;
        if (screenshotCount >= textureDataArray.Length)
        {
            screenshotCount = 0;
        }
    }

    public void ResetVariables()
    {
        isFlyCam = false;
        coins = 0;
        isCameraBought = false;
        textureDataArray = new TextureData[3];
        AvatarID = 0;
        RunsCompleted = 0;
        cameraBoughtFirstTimeMenu = false;
        screenshotCount = 0;
    }
}