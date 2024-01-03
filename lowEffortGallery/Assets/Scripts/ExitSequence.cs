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
            if (_playableDirector != null)
                _playableDirector.Play();
            else
                Exit();
        }
    }

    public void Exit()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
