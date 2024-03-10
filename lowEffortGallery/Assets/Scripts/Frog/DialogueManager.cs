using UnityEngine;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private DialoguePrinter _dialoguePrinter;
    string[] phrase = new string[] {""};
    private bool isSayPhrase = true;
    public int freeCamCost = 5;
    
    
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
        if (isSayPhrase)
        {
            DialoguePrinter.instance.NewSay(phrase);
        }
    }
    
    private void ChoosePhrase(string key)
    {
        switch (key)
        {
            case "start":
                phrase = new string[]
                {
                    "Yo, welcome to the haze gift shop, fam! We're like nestled in the loading bay between two spots",
                    "Here you can cop pre-made tourist shots with you in the mix, and also snag a special souvenir.. ",
                    "the Free Cam, letting you soar through all the halls!",
                    "I'm straight up urging you to cop this cam to peep the whole gallery content and the ghost gallery!",
                    "Checking your cash flow..."
                };
                if (GameVariables.instance.coins >= freeCamCost)
                {
                    phrase = phrase.Concat(new string[]
                    {
                        "I'm sniffin' that money scent! You're just right on the mark for the Free Cam!",
                        "Cop it and ride the sky, feelin' free! BUY IT, MY HOMIE!"
                    }).ToArray();
                }
                else if (GameVariables.instance.coins < freeCamCost && GameVariables.instance.coins != 0)
                {
                    phrase = phrase.Concat(new string[]
                    {
                        "I'm sniffin' that money scent! Unfortunately, you're short on cash for the  free cam",
                        "You can stroll through the gallery again, scooping up them missing coins from the menu",
                        "All the coins you gather stay safe till you're fully out of the GAME!"
                    }).ToArray();
                }
                else
                {
                    phrase = phrase.Concat(new string[]
                    {
                        "Yeah mate, cash flow's practically non-existent",
                        "Gotta hustle those yellow coins at the jump and then come back to splash 'em like a boss"
                    }).ToArray();
                }
                break;
                
            case "startAgain":
                string[] variants = new string[]{ "Yo, what's good? Welcome back, yo!",
                    "New day, new load-up, new snaps",
                };
                phrase = new string[] {RandomExtensions.GetRandomElement(variants)};
                if (!GameVariables.instance.isCameraBought)
                {
                    phrase = phrase.Concat(new string[] { "Checking your cash flow..." }).ToArray();
                    {
                        if (GameVariables.instance.coins >= freeCamCost)
                        {
                            phrase = phrase.Concat(new string[]
                            {
                                "Smellin' that money vibe!",
                                "You're just right on the mark for the Free Cam!",
                                "Cop it and ride the sky, feelin' free! COP IT, MY DUDE!"
                            }).ToArray();
                        }
                        else if (GameVariables.instance.coins < freeCamCost && GameVariables.instance.coins != 0)
                        {
                            //дописать для бабок меньше стоимости камеры типа денег хватает купить фоток
                            phrase = phrase.Concat(new string[]
                            {
                                "",
                                "",
                                ""
                            }).ToArray();
                        }
                        else
                        {
                            //и отсутствия бабок
                            phrase = phrase.Concat(new string[]
                            {
                                "",
                                "",
                                ""
                            }).ToArray();
                        }
                    }
                }
                
                
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
