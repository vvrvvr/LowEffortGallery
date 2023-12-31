using UnityEngine;

public class TurnOffMeshOnCollision : MonoBehaviour
{
    private bool _isOnce = true;
    public GameObject playerMesh;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isOnce && !GameManager.Instance.isFlyCam)
        {
            _isOnce = false;
            playerMesh.SetActive(false);
            Destroy(gameObject);
            Debug.Log("collision");
        }
    }
}
