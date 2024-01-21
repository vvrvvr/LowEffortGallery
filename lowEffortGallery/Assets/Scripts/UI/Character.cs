using UnityEngine;

public class Character : MonoBehaviour
{
    public ChooseCharacter _chooseCharacter;
    public int characterId = 0;
    private float delay = 1f;
    private void OnEnable()
    {
        EventManager.OnNewGame.AddListener(ReleaseAvatar );
    }

    private void OnDisable()
    {
        EventManager.OnNewGame.RemoveListener(ReleaseAvatar);
    }

    private void ReleaseAvatar()
    {
        if (GameVariables.instance.AvatarID == characterId)
        {
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            StartCoroutine(DestroyAvatarWithDelay(delay));
        }
    }
    private System.Collections.IEnumerator DestroyAvatarWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            _chooseCharacter.StopRotation(characterId);
            GameVariables.instance.AvatarID = characterId;
        }
    }
}
