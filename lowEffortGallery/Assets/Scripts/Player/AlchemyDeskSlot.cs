using System.Collections;
using UnityEngine;

public class AlchemyDeskSlot : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private Transform itemPlace;
    private InterfaceObject currentInterface = null;
    private Coroutine wait;
        

    public bool isSlotBusy = false;

    void Start()
    {
        outline.SetActive(false);
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
        currentInterface.DeleteInterface();
        currentInterface = null;
        isSlotBusy = false;
    }
}