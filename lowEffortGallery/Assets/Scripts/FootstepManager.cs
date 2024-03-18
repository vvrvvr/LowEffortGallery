using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] Steps;
    public float pitchMax = 1.0f;
    public float pitchMin = 0.8f;
    public bool isPitch = true;
    public float playStepDelay = 0.5f;

    private AudioSource audioSource;
    public AudioSource audioSourceJump;
    //public AudioClip jumpSound;
    public float playJumpDelay = 0.5f;
    private bool canPlay = true;
    private float lastStepTime = 0f;
    private bool canChooseSteps = false;
    private AudioClip step;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }

        if (Steps.Length > 1)
        {
            canChooseSteps = true;
        }
        else
        {
            step = Steps[0];
            audioSource.clip = step;
        }
    }

    public void PlayFootstep()
    {
        if (Time.time - lastStepTime >= playStepDelay)
        {
            if (canChooseSteps)
            {
               step = Steps[Random.Range(0, Steps.Length)];
               audioSource.clip = step;
            }
            if (isPitch)
            {
                float pitch = Random.Range(pitchMin, pitchMax);
                audioSource.pitch = pitch;
            }

            audioSource.Play();
            lastStepTime = Time.time;
        }
    }

    public void PlayJump()
    {
        if (Time.time - lastStepTime >= playJumpDelay)
        {
            audioSourceJump.Play();
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