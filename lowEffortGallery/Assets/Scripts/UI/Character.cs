using UnityEngine;

public class Character : MonoBehaviour
{
    public ChooseCharacter _chooseCharacter;
    public int characterId = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            _chooseCharacter.StopRotation(characterId);
        }
    }
}
