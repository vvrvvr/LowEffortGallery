using UnityEngine;

public class PlaceDialogueTrigger : MonoBehaviour
{
    private bool isOnce = true;
    public bool withDelay = false;
    public string[] Phrase = new string[1];
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            isOnce = false;
            if (withDelay)
            {
                DialoguePrinter.instance.NewSay(Phrase, true);
            }
            else
            {
                DialoguePrinter.instance.NewSay(Phrase);
            }
            
        }
    }
}
