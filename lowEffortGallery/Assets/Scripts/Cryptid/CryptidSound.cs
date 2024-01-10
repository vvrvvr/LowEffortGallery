using UnityEngine;

public class CryptidSound : MonoBehaviour
{
    
    private bool isOnce = true;
    private AudioSource _audio;

    private void Start()
    {
        _audio = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            Debug.Log("cryptid sound");
            isOnce = false;
            _audio.Play();
            
        }
    }
   
}
