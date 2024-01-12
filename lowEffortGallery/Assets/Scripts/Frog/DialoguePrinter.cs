using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialoguePrinter : MonoBehaviour
{
    public static DialoguePrinter instance;
    public Elements elements;

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

    public float textSpeed = 0.02f; // Скорость вывода букв

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        string[] doorPhrase = new string[] { "Цветов слишком мало для букета", "Цветоsdfsssв слишком мало для букета", "ddsf f f " };
        NewSay(doorPhrase);
        //Say("слишком мало для буке");
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (Input.GetMouseButtonDown(0) || startSpeaking)
            {
                startSpeaking = false;
                if (!isSpeaking || isWaitingForUserInput)
                {
                    if (index >= str.Length)
                    {
                        if (offPanel != null)
                            StopCoroutine(offPanel);
                        offPanel = null;
                        SwitchOffTextPanel();
                        return;
                    }

                    Say(str[index]);
                    index++;
                }
                else if (isSpeaking)
                {
                    // Если фраза выводится, завершить вывод сразу
                    StopSpeaking();
                    SpeechText.text = str[index-1]; // Отобразить фразу целиком
                    isWaitingForUserInput = true;
                }
            }
        }
    }

    public void NewSay(string[] s)
    {
        StopSpeaking();
        str = s;
        startSpeaking = true;
        isDialogue = true;
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
        SpeechPanel.SetActive(true);
        targetSpeech = speech;
        SpeechText.text = "";

        isWaitingForUserInput = false;
        while (SpeechText.text != speech)
        {
            SpeechText.text += targetSpeech[SpeechText.text.Length];
            yield return new WaitForSeconds(textSpeed); // Используйте заданную скорость вывода букв
        }

        isWaitingForUserInput = true; // Позволяет игроку пропустить текущую фразу
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        offPanel = StartCoroutine(SwitchOffTextPanelTimer());
        StopSpeaking();
    }

    IEnumerator SwitchOffTextPanelTimer()
    {
        yield return new WaitForSeconds(4.0f);
        SwitchOffTextPanel();
    }

    private void SwitchOffTextPanel()
    {
        SpeechPanel.SetActive(false);
        isDialogue = false;
        index = 0;
        if (offPanel != null)
            StopCoroutine(offPanel);
        offPanel = null;
    }
}
