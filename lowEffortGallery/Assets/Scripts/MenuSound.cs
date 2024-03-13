using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource hoverSound;
    public AudioSource clickSound;
    public AudioSource chooseSound;
    
    public void PlayHover(){
        hoverSound.Play();
    }
    public void PlayClick(){
        clickSound.Play();
    }
    public void PlayChoose(){
        chooseSound.Play();
    }
}
