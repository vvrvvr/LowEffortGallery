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
    public Texture2D maskTexture;

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

        // Increment the screenshot count
        screenshotCount++;

        cameraToCapture.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Apply the mask texture
        ApplyMaskToTexture();

        cameraToCapture.gameObject.SetActive(false);
        Debug.Log("screenshot made");
    }
    private void ApplyMaskToTexture()
    {
        if (savedTexture == null || maskTexture == null)
        {
            Debug.LogError("savedTexture or maskTexture is null. Make sure they are initialized.");
            return;
        }

        // Make sure the mask texture has the same dimensions as the captured image
        maskTexture = ResizeTexture(maskTexture, savedTexture.width, savedTexture.height);

        // Apply the mask texture
        Color[] pixels = savedTexture.GetPixels();
        Color[] maskPixels = maskTexture.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            // If the mask pixel is not fully black, replace the corresponding pixel in the saved texture
            if (maskPixels[i].r > 0 || maskPixels[i].g > 0 || maskPixels[i].b > 0)
            {
                pixels[i] = maskPixels[i];
            }
        }

        savedTexture.SetPixels(pixels);
        savedTexture.Apply();
    }

    private Texture2D ResizeTexture(Texture2D texture, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        rt.filterMode = FilterMode.Bilinear;
        RenderTexture.active = rt;
        Graphics.Blit(texture, rt);
        Texture2D resizedTexture = new Texture2D(width, height);
        resizedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        resizedTexture.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return resizedTexture;
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
        // Save texture to file
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), folderName, fileNamePrefix + screenshotCount.ToString() + ".png");
        byte[] bytes = savedTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }
}
