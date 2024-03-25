using UnityEngine;

public class CryptidSound : MonoBehaviour
{
    public float pitchMin = 0.6f;
    public float pitchMax = 1f;
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
            float pitch = Random.Range(pitchMin, pitchMax);
            _audio.pitch = pitch;
            isOnce = false;
            _audio.Play();
        }
    }
   
}
