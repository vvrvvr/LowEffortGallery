using UnityEngine;

public class FlyCameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject flyCam;
    public GameObject FlyCameraPriceTag;
    void Start()
    {
        if (GameVariables.instance.isCameraBought)
        {
            if(flyCam!=null)
                flyCam.SetActive(false);
            if(FlyCameraPriceTag!=null)
                FlyCameraPriceTag.SetActive(false);
        }
    }
    
}
