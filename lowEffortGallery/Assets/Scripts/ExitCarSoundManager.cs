using UnityEngine;
using DG.Tweening;

public class ExitCarSoundManager : MonoBehaviour
{
    public AudioSource CarAudioSource;
    public AudioClip carDoorsOpen;
    public AudioClip carEngineWroom;
    public float delayToDoorsSound = 1f;
    public float delayToWroomSound = 2f;

    public void StartAudioSequence()
    {
        DOTween.Sequence()
            .AppendInterval(delayToDoorsSound)
            .AppendCallback(() => PlaySound(carDoorsOpen))
            .Play();
        
        DOTween.Sequence()
            .AppendInterval(delayToDoorsSound + delayToWroomSound)
            .AppendCallback(() => PlaySound(carEngineWroom))
            .Play();
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            CarAudioSource.clip = clip;
            CarAudioSource.Play();
        }
    }
}