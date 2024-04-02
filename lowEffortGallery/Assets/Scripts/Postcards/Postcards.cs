using UnityEngine;

public class Postcards : MonoBehaviour
{
    public GameObject[] _postcards = new GameObject[3];
    public GameObject[] _priceTags = new GameObject[3];
    

    private void Start()
    {
        var photos = GameManager.Instance.texturesArray;
        int postcardCounter = 0;
        int photoCounter = 0;
        for (; photoCounter < photos.Length; photoCounter++)
        {
            if (photos[photoCounter].texture != null)
            {
                var postcardInterfaceObject = _postcards[postcardCounter].GetComponent<InterfaceObject>();
                postcardInterfaceObject.photoArrayID = photoCounter;
                postcardInterfaceObject.avatar = photos[photoCounter].avatar;
                postcardInterfaceObject.hall = photos[photoCounter].hall;
                
                ApplySavedTexture(photos[photoCounter].texture,_postcards[postcardCounter]);
                postcardCounter++;
            }
        }

        for (; postcardCounter < _postcards.Length; postcardCounter++)
        {
            _postcards[postcardCounter].SetActive(false);
            _priceTags[postcardCounter].SetActive(false);
        }
    }
    
    private void ApplySavedTexture(Texture2D choosenTexture, GameObject photoObject)
    {
        // Apply the saved texture to the photoObject
        Renderer renderer = photoObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = choosenTexture;
            Debug.Log("Texture applied to photoObject");
        }
        else
        {
            Debug.LogError("photoObject does not have a Renderer component");
        }
    }
}
