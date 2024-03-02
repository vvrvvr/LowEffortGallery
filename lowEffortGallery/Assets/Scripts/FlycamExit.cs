using UnityEngine;

public class FlycamExit : MonoBehaviour
{
    private bool isOnce = true; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOnce)
            {
                Debug.Log("Exit fly cam");
                isOnce = false;
                GameManager.Instance.LoadNextLevel("Start");
            }
        }
    }
}
