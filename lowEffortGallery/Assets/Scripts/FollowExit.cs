using UnityEngine;

public class FollowExit : MonoBehaviour
{
    public Transform anchor;

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
