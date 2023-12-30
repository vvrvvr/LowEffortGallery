using UnityEngine;
using UnityEngine.Playables;

public class ExitSequence : MonoBehaviour
{
    public PlayableDirector _playableDirector;
    private bool isOnce = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            isOnce = false;
            Debug.Log("StartSequence");
            _playableDirector.Play();
        }
    }

    public void Exit()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
