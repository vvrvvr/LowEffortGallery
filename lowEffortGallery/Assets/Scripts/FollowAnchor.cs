using System;
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    public Transform anchor;
    private bool _isFollow = true;
    public GameObject[] Avatars = new GameObject[3];
   // public float delayTime = 1.0f; // Задержка в секундах

    private void Start()
    {
        // if (GameManager.Instance.isFlyCam)
        // {
        //    // _isFollow = false;
        // }
    }
    
    private void LateUpdate()
    {
        if (!_isFollow)
            return;
        
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
    }

    public void SetupAvatars(int numb, float delay)
    {
        StartCoroutine(ActivateAvatarWithDelay(numb, delay));
    }

    public void HideAvatars()
    {
        for (int i = 0; i < Avatars.Length; i++)
        {
            Avatars[i].SetActive(false);
        }
    }

    private System.Collections.IEnumerator ActivateAvatarWithDelay(int numb, float delay)
    {
        for (int i = 0; i < Avatars.Length; i++)
        {
            if (i != numb)
            {
                Avatars[i].SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(delay);
                Avatars[i].SetActive(true);
                break;
            }
        }
    }
}