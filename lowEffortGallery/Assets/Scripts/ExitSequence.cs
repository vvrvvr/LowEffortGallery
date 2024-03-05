using UnityEngine;
using UnityEngine.Playables;

public class ExitSequence : MonoBehaviour
{
    public PlayableDirector _playableDirector;
    private bool isOnce = true;
    public string nextLevelName = "Start";
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
        Debug.Log("Reach Exit");
        GameManager.Instance.LoadNextLevel(nextLevelName);
    }
}
