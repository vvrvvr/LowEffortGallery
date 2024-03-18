using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] Steps;
    public float pitchMax = 1.0f;
    public float pitchMin = 0.8f;
    public bool isPitch = true;
    public float playStepDelay = 0.5f;

    private AudioSource audioSource;
    private bool canPlay = true;
    private float lastStepTime = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }
    }

    public void PlayFootstep()
    {
        if (Time.time - lastStepTime >= playStepDelay)
        {
            AudioClip step = Steps[Random.Range(0, Steps.Length)];
            audioSource.clip = step;

            if (isPitch)
            {
                float pitch = Random.Range(pitchMin, pitchMax);
                audioSource.pitch = pitch;
            }

            audioSource.Play();
            lastStepTime = Time.time;
        }
    }

    // private void Update()
    // {
    //     if (!canPlay && !audioSource.isPlaying)
    //     {
    //         canPlay = true;
    //     }
    // }
}