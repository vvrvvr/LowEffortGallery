using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip musicClip;
    private double startTime;

    public void FadeSound(float fadeSpeed, bool isStart)
    {
        foreach (AudioSource source in audioSources)
        {
            float targetVolume = isStart ? source.volume : 0f;
            if (isStart)
            {
                source.volume = 0f;
            }

            source.DOFade(targetVolume, fadeSpeed);
        }
    }

    public void PlayAudioSynced(AudioClip clip, double delay)
    {
        double playTime = AudioSettings.dspTime + delay;
        foreach (AudioSource source in audioSources)
        {
            source.clip = clip;
            source.PlayScheduled(playTime);
        }
    }

    private void Start()
    {
        PlayAudioSynced(musicClip, 0.1);
        FadeSound(2f, true);
    }
}