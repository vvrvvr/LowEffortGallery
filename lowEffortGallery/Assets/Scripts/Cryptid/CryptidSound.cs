using UnityEngine;

public class CryptidSound : MonoBehaviour
{
    
    private bool isOnce = true;

    private void Start()
    {
        // MeshRenderer currentMeshRenderer = GetComponent<MeshRenderer>();
        // if (currentMeshRenderer != null)
        // {
        //     currentMeshRenderer.enabled = false;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            Debug.Log("cryptid sound");
            isOnce = false;
            
        }
    }
   
}
