using System;
using UnityEngine;
using System.IO;

public class CaptureScreenshot : MonoBehaviour
{
    public Camera cameraToCapture;
    public GameObject photoObject;
    public string folderName = "galleryFiles";
    public string fileNamePrefix = "Screenshot";

    private int screenshotCount = 1;
    private Texture2D savedTexture;

    private void Start()
    {
        cameraToCapture.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed");
            MakeScreenshot();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (savedTexture != null)
            {
                ApplySavedTexture();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("saved");
            SaveTextureToFile();
        }
    }

    public void MakeScreenshot()
    {
        cameraToCapture.gameObject.SetActive(true);

        
        // Capture the screenshot
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraToCapture.targetTexture = renderTexture;
        cameraToCapture.Render();
        RenderTexture.active = renderTexture;
        savedTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        savedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        savedTexture.Apply();
        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        
        // Increment the screenshot count
        screenshotCount++;

        cameraToCapture.gameObject.SetActive(false);
        Debug.Log("screenshot made");
        
    }

    private void ApplySavedTexture()
    {
        // Apply the saved texture to the photoObject
        Renderer renderer = photoObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = savedTexture;
            Debug.Log("Texture applied to photoObject");
        }
        else
        {
            Debug.LogError("photoObject does not have a Renderer component");
        }
    }

    public void SaveTextureToFile()
    {
        // Create the folder if it does not exist
        string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        //save texture to file
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName, fileNamePrefix + screenshotCount.ToString() + ".png");
        byte[] bytes = savedTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }
    
}
