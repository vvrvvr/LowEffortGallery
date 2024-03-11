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
                        "Yo fam, I'm smelling that cash flow! But yo, unfortunately, your cam game's a bit low",
                        "You can cop some pics or run through the  not gallery again, gathering them extra coins in the menu",
                        "All them coins you gather stay stacked till you bounce out the GAME for real"
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
                                "Yo yo yo, cash only stretches to cop some pics, fam!",
                                "You gotta hustle up, stacking them coins in the menu...",
                                "and speedrun through the gallery again, ya dig"
                            }).ToArray();
                        }
                        else
                        {
                            //и отсутствия бабок
                            phrase = phrase.Concat(new string[]
                            {
                                "Damn, fam, you're straight-up broke",
                                "Capitalism got us all cursed..."
                            }).ToArray();
                        }
                    }
                }
                
                
                break;
                
            case "cantBuyCam":
                if (cantBuyCam)
                {
                    phrase = new string[]
                    {
                        "Yeah, solid gear, but dang, short on cash",
                        "Quick run-through the gallery again, grabbin' them coins upfront"
                    };
                    cantBuyCam = false;
                }
                else
                {
                    var variants1 = new string[]{ 
                        "Nah, still ain't there.",
                        "Damn, not enough",
                        "Damn capitalism..."
                    };
                    phrase = new string[] {RandomExtensions.GetRandomElement(variants1)};
                }
                break;
                
            case "cameraBought":
                phrase = new string[]
                {
                    "Yo, this is the ultimate cash splash!",
                    "You can flip to the Free Cam whenever in the pause menu (Escape button)",
                    "Every purchase stays locked until you fully bounce from the gallery!"
                };
                break;
            
            case "cantBuyPhoto":
                if (cantBuyPhoto)
                {
                    phrase = new string[]
                    {
                        "Man, strapped for cash, darn it. In the virtual realm, it's all tight."
                    };
                    cantBuyPhoto = false;
                }
                else
                {
                    var variants2 = new string[]{ 
                        "Nah, still ain't there.",
                        "Damn, not enough",
                        "Damn capitalism...",
                        "We're living in this cursed post-capitalist world, even gotta hustle in the virtual not-gallery"
                    };
                    phrase = new string[] {RandomExtensions.GetRandomElement(variants2)};
                }
                break;
            
            case "photoBought":
                if (photoBought)
                {
                    phrase = new string[]
                    {
                        "Solid choice!",
                        "Drop the photo on the counter for that expanded-magic to happen and save it to your desktop!",
                        "If all's smooth sailing, you should see a folder with our gallery's name and files inside on your desk"
                    };
                    photoBought = false;
                }
                else
                {
                    var variants3 = new string[]{ 
                        "Not bad. Chuck the photo into the virtual grinder to save it to your desktop",
                        "The photo's yours. You can do whatever with it within our gallery's limits"
                    };
                    phrase = new string[] {RandomExtensions.GetRandomElement(variants3)};
                }
                break;
                
            case "playerTeleport":
                if (playerTeleport)
                {
                    phrase = new string[]
                    {
                        "Perks of the bay - you can only stroll around in the souvenir shop",
                        "Heard with the Free Cam, you might snag a brief getaway beyond the bay"
                    };
                    playerTeleport = false;
                }
                else
                {
                    var variants4 = new string[]{ 
                        "Just another teleportation",
                        "If things are normal, you'll just keep getting teleported back",
                        "Whoa, dope teleportation!"
                    };
                    phrase = new string[] {RandomExtensions.GetRandomElement(variants4)};
                }
                break;
            
            case "photoSavedToDesktop":
                if (photoSavedToDesktop)
                {
                    phrase = new string[] {"Looks like it's saved. At least, I really tried to make it stick"};
                    photoSavedToDesktop = false;
                }
                else
                {
                    phrase = new string[] {"Check in the folder on your desktop"};
                }
                break;
            
            default:
                phrase = new string[] {"Some glitches in the code. We're hustling hard to fix 'em up"};
                break;
        }
    }
    
}
