using UnityEngine;

public class FollowExit : MonoBehaviour
{
    public Transform anchorController;
    
    void Update()
    {
        Vector3 anchorPosition = anchorController.position;
            anchorPosition.y = transform.position.y;
            transform.position = anchorPosition;
        
    }
    
}
