using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public GameObject[] _photoSpots = new GameObject[3];
    void Start()
    {
        for (int i = 0; i < _photoSpots.Length; i++)
        {
            _photoSpots[i].SetActive(false);
        }

        var spot = RandomExtensions.GetRandomElement(_photoSpots);
        spot.SetActive(true);
    }

   
}
