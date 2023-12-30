using System;
using UnityEngine;

public class FollowAnchor : MonoBehaviour
{
    public Transform anchor;
    private bool _isFollow = true;

    private void Start()
    {
        if (GameManager.Instance.isFlyCam)
        {
            _isFollow = false;
        }
    }
    
    private void LateUpdate()
    {
        if (!_isFollow)
            return;
        
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
    }
}