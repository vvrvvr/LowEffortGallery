using System.Collections;
using UnityEngine;

public class AlchemyDeskSlot : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private Transform itemPlace;
    private InterfaceObject currentInterface = null;
    private Coroutine wait;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator counterAnimator;
    public ParticleSystem particles;
    public QRCodeHandler qrCodeHandler;
        

    public bool isSlotBusy = false;

    void Start()
    {
        outline.SetActive(false);
        animator.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.OnItemHeld.AddListener(OutlineOn);
        EventManager.OnItemDroped.AddListener(OutlineOff);
    }

    private void OnDisable()
    {
        EventManager.OnItemHeld.RemoveListener(OutlineOn);
        EventManager.OnItemDroped.AddListener(OutlineOff);
    }

    private void OutlineOn()
    {
        if (!isSlotBusy)
            outline.SetActive(true);
    }

    private void OutlineOff()
    {
        if (!isSlotBusy )
            outline.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interface") && !isSlotBusy)
        {
            currentInterface = other.GetComponent<InterfaceObject>();
            if (!currentInterface._isDropped)
                return;
            currentInterface.MoveInterfaceToSlot(0f, 0.4f, itemPlace );
            currentInterface.SetInHandScale(1.5f, 10f, 100f);
            isSlotBusy = true;
            currentInterface.rotationSpeed = 100f;
            wait = StartCoroutine(WaitToBuy(2f));
        }
    }

    IEnumerator WaitToBuy(float time)
    {
        yield return new WaitForSeconds(time);
        //здесь обработка результата покупки
        ApplyBuy();
    }
    
       
    public void ApplyBuy()
    {
        if (GameVariables.instance.isExposition)
        {
            int avatar = currentInterface.avatar;
            int hall = currentInterface.hall;
            //здесь изменить фразу и отправить в гейм менеджер для показа куар кода 
            var qrCodeIndex = GetCode(avatar, hall);
            qrCodeHandler.ApplyCode(qrCodeIndex);
            Debug.Log(GetCode(avatar, hall));
            DialogueManager.instance.FrogSay("photoSavedToQR");
        }
        else
        {
            DialogueManager.instance.FrogSay("photoSavedToDesktop");
            GameManager.Instance.SaveTextureToFile(currentInterface.photoArrayID);
        }
        currentInterface.DeleteInterface();
        animator.enabled = false;
        animator.enabled = true;
        counterAnimator.enabled = false;
        counterAnimator.enabled = true;
        particles.Play();
        currentInterface = null;
        isSlotBusy = false;
    }
    
    public int GetCode(int avatar, int hall)
    {
        if (avatar < 0 || avatar > 2 || hall < 1 || hall > 3)
        {
            Debug.LogError("Недопустимые значения avatar или hall.");
            return -1;
        }

        // Массив изображений имеет размер 3x3, поэтому используем формулу:
        // индекс = avatar * 3 + (hall - 1)
        int code = avatar * 3 + (hall - 1);
    
        return code;
    }
    
}