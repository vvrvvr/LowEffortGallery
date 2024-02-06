using UnityEngine;

public class CaptureScreenshot : MonoBehaviour
{
    [SerializeField] private PhotoWarning _photoWarning;
    public Camera cameraToCapture;
    public Camera[] camerasToCaptureList = new Camera[0];
    public Texture2D maskTexture;
    private Texture2D savedTexture;
    public bool isApplyMaskToPhoto = true;
    private bool isOnce = true;
    

    private void Start()
    {
        if (camerasToCaptureList.Length !=0 && camerasToCaptureList[0] != null )
        {
            cameraToCapture = RandomExtensions.GetRandomElement(camerasToCaptureList);
            Debug.Log("Executed");
        }
        
    }

    private void Update() //удалить метод
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed P for photo");
            MakeScreenshot();
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            isOnce = false;
            MakeScreenshot();
        }
    }

    public void MakeScreenshot()
    {
        cameraToCapture.gameObject.SetActive(true);
        _photoWarning.DisableWarning();
        
        GameManager.Instance.PhotoFlash();

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

        // Apply the mask texture
        if(isApplyMaskToPhoto)
            ApplyMaskToTexture();

        cameraToCapture.gameObject.SetActive(false);
        
        GameManager.Instance.SavePhotoTextureToArray(savedTexture);
        savedTexture = null;
        
        Debug.Log("screenshot made");
    }
    private void ApplyMaskToTexture()
    {
        if ( maskTexture == null)
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
            // If the mask pixel is not alpha, replace the corresponding pixel in the saved texture
            if (maskPixels[i].a != 0)
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
   
}
