using System;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class CaptureScreenshot : MonoBehaviour
{
    public Camera cameraToCapture;
    public string folderName = "galleryFiles";
    public string fileNamePrefix = "Screenshot";

    private int screenshotCount = 1;

    private void Start()
    {
        // Create the folder if it does not exist
        string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        cameraToCapture.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed");
            MakeScreenshot();
        }
            
    }

    public void MakeScreenshot()
    {
        cameraToCapture.gameObject.SetActive(true);
        // Set the file path
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName, fileNamePrefix + screenshotCount.ToString() + ".png");

        // Capture the screenshot
        // Capture the screenshot from the selected camera
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraToCapture.targetTexture = renderTexture;
        cameraToCapture.Render();
        RenderTexture.active = renderTexture;
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        byte[] bytes = screenshotTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        // Increment the screenshot count
        screenshotCount++;
        cameraToCapture.gameObject.SetActive(false);
        Debug.Log("screenshot made");
    }
}