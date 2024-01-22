using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ChooseCharacter _chooseCharacter;
    private Vector3 startPosition;
    public int characterId = 0;
    private float delay = 1f;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

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
            StartCoroutine(DeactivateAvatarWithDelay(delay, rb));
        }
    }
    private System.Collections.IEnumerator DeactivateAvatarWithDelay(float delay, Rigidbody rb)
    {
        yield return new WaitForSeconds(delay);
        
        rb.isKinematic = true;
        transform.localPosition = startPosition;
        gameObject.SetActive(false);
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
