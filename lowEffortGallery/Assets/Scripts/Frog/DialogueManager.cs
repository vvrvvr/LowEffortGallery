using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private DialoguePrinter _dialoguePrinter;
    string[] phrase = new string[] {""};
    private void Awake()
    {
        instance = this;
    }

    public void FrogSay(string key)
    {
        phrase = null;
        ChoosePhrase(key);
        _dialoguePrinter.NewSay(phrase);
    }
    
    private void ChoosePhrase(string key)
    {
        switch (key)
        {
            case "start":
                phrase = new string[] {"yoooo start phrase test"};
                break;
                
            case "startAgain":
                phrase = new string[] {"start again phrase test"};
                break;
            
            default:
                phrase = new string[] {"yo"};
                break;
        }
    }
    
}
