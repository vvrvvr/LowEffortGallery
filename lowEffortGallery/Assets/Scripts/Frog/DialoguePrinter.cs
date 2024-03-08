using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Linq;

public class DialoguePrinter : MonoBehaviour
{
    public static DialoguePrinter instance;
    public Elements elements;
    public GameObject DelayObj;
    public float delayTime = 5f;
    private Sequence delaySequence = null;

    [System.Serializable]
    public class Elements
    {
        public GameObject SpeechPanel;
        public TextMeshProUGUI SpeechText;
    }

    public bool isSpeaking { get { return speaking != null; } }
    public GameObject SpeechPanel { get { return elements.SpeechPanel; } }
    public TextMeshProUGUI SpeechText { get { return elements.SpeechText; } }

    private int index = 0;
    private string[] str;
    private bool startSpeaking = false;
    [HideInInspector] public bool isWaitingForUserInput;
    private string targetSpeech = "";
    private Coroutine speaking = null;
    private Coroutine offPanel = null;

    private bool isDialogue;
    public bool isDialogueCantInteract = false;

    public float textSpeed = 0.02f;
    public float dialogueDelay = 2.0f; // Время задержки после завершения написания фразы
    string inbetween = "between";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DelayObj.SetActive(false);
        SpeechPanel.SetActive(false);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     string[] doorPhrase = new string[] { "I fixed this by generating a new font asset and increasing the padding from 5 to 10.", "thats all" };
        //     NewSay(doorPhrase);
        // }
        if (isDialogue)
        {
            if ((Input.GetMouseButtonDown(0) && !GameManager.Instance.isPause) || startSpeaking)
            {
                delaySequence.Kill();
                startSpeaking = false;
                if (!isSpeaking || isWaitingForUserInput)
                {
                    if (index >= str.Length)
                    {
                        if (offPanel != null)
                            StopCoroutine(offPanel);
                        offPanel = null;
                        StartCoroutine(SwitchOffTextPanelTimer());
                        //SwitchOffTextPanel();
                        return;
                    }

                    Say(str[index]);
                    index++;
                }
                else if (isSpeaking)
                {
                    delaySequence.Kill();
                    AutoEndPhrase();
                    
                    DelayObj.SetActive(true);
                    StopSpeaking();
                    SpeechText.text = str[index-1];
                    isWaitingForUserInput = true;
                }
            }
        }
    }

    public void NewSay(string[] s)
    {
        //StopSpeaking();
        if (isSpeaking)
        {
            str = str.Concat(new string[] { inbetween }).Concat(s).ToArray();
        }
        else
        {
            str = s;
            startSpeaking = true;
            isDialogue = true;
            isDialogueCantInteract = true;
        }
    }
    public void NewSay(string[] s, bool isDelay)
    {
        //StopSpeaking();
        if (isSpeaking)
        {
            str = str.Concat(new string[] { inbetween }).Concat(s).ToArray();
        }
        else
        {
            str = s;
            startSpeaking = true;
            isDialogue = true;
            isDialogueCantInteract = true;
        }
    }

    public void AutoEndPhrase()
    {
        Debug.Log("start wait");
         delaySequence = DOTween.Sequence()
            .AppendInterval(delayTime)
            .OnComplete(() => {
                startSpeaking = true;
                Debug.Log("end wait");
            });
    }

    public void Say(string speech)
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech));
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
        
    }

    IEnumerator Speaking(string speech)
    {
        DelayObj.SetActive(false);
        SpeechPanel.SetActive(true);
        targetSpeech = speech;
        SpeechText.text = "";

        isWaitingForUserInput = false;
        while (SpeechText.text != speech)
        {
            SpeechText.text += targetSpeech[SpeechText.text.Length];
            yield return new WaitForSeconds(textSpeed);
        }
        
        DelayObj.SetActive(true);
        AutoEndPhrase();
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        offPanel = StartCoroutine(SwitchOffTextPanelTimer());
        StopSpeaking();
    }

    IEnumerator SwitchOffTextPanelTimer()
    {
        
        SwitchOffTextPanel();
        yield return new WaitForSeconds(0.2f);
        isDialogueCantInteract = false;
        //yield return new WaitForSeconds(0.5f);
    }

    private void SwitchOffTextPanel()
    {
        DelayObj.SetActive(false);
        SpeechPanel.SetActive(false);
        isDialogue = false;
        
        index = 0;
        if (offPanel != null)
            StopCoroutine(offPanel);
        offPanel = null;
    }
}
