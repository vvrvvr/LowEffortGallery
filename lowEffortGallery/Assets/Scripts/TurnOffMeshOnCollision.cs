using UnityEngine;

public class TurnOffMeshOnCollision : MonoBehaviour
{
    private bool _isOnce = true;
    //public GameObject playerMesh;
    public FollowAnchor avatars;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isOnce && !GameManager.Instance.isFlyCam)
        {
            _isOnce = false;
           avatars.HideAvatars();
            Destroy(gameObject);
            Debug.Log("collision");
        }
    }
}
