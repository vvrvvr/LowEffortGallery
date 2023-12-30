using UnityEngine;

public class FollowExit : MonoBehaviour
{
    public Transform anchorDefaultController;
    private Transform anchor;
    public Transform anchorFlyCam;

    private void Start()
    {
        if (GameManager.Instance.isFlyCam)
        {
            anchor = anchorFlyCam;
        }
        else
        {
            anchor = anchorDefaultController;
        }
    }

    void Update()
    {
        if (anchor != null)
        {
            Vector3 anchorPosition = anchor.position;
            anchorPosition.y = transform.position.y;
            transform.position = anchorPosition;
        }
    }
    
}
