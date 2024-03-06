using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private DialoguePrinter _dialoguePrinter;
    string[] phrase = new string[] {""};
    private bool isSayPhrase = true;
    
    
    //bools for counting phrase tries
    private bool cantBuyCam = true;
    private bool cantBuyPhoto = true;
    private bool photoBought = true;
    private bool playerTeleport = true;
    private bool photoSavedToDesktop = true;
    
    
    private void Awake()
    {
        instance = this;
    }

    public void FrogSay(string key)
    {
        phrase = null;
        isSayPhrase = true;
        ChoosePhrase(key);
        if(isSayPhrase)
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
                
            case "cantBuyCam":
                if (cantBuyCam)
                {
                    phrase = new string[] {"cant buy cam first"};
                    cantBuyCam = false;
                }
                else
                {
                    phrase = new string[] {"cant buy cam next try"};
                }
                break;
                
            case "cameraBought":
                phrase = new string[] {"camera bought"};
                break;
            
            case "cantBuyPhoto":
                if (cantBuyPhoto)
                {
                    phrase = new string[] {"cantBuyPhoto"};
                    cantBuyPhoto = false;
                }
                else
                {
                    phrase = new string[] {"cantBuyPhoto next try"};
                }
                break;
            
            case "photoBought":
                if (photoBought)
                {
                    phrase = new string[] {"photo bought"};
                    photoBought = false;
                }
                else
                {
                    phrase = new string[] {"photo bought again"};
                }
                break;
                
            case "playerTeleport":
                if (playerTeleport)
                {
                    phrase = new string[] {"first teleport"};
                    playerTeleport = false;
                }
                else
                {
                    phrase = new string[] {"next teleports"};
                }
                break;
            
            case "photoSavedToDesktop":
                if (photoSavedToDesktop)
                {
                    phrase = new string[] {"first photoSaved"};
                    photoSavedToDesktop = false;
                }
                else
                {
                    phrase = new string[] {"next photo saved"};
                }
                break;
            
            default:
                phrase = new string[] {"yo, some problems with code. we work hard to fix it"};
                break;
        }
    }
    
}
