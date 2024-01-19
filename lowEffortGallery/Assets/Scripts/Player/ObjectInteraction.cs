using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public float pickupDistance = 10f;
    public GameObject heldObject = null;
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Transform heldObjectLocalPosition;
    [SerializeField] private float impulseForce = 1f;
    [SerializeField] private GameObject cursor;
    
    private CharacterController _characterController;

    private Camera _camera;

    //
    [SerializeField] private Image pointerImage;
    [SerializeField] private Sprite crosshair;
    [SerializeField] private Sprite hand;
    private bool isPause = false;

    private void OnEnable()
    {
        EventManager.OnPause.AddListener(()=> isPause = true);
        EventManager.OnResumeGame.AddListener(()=> isPause = false);
    }

    private void OnDisable()
    {
        EventManager.OnPause.RemoveListener(()=> isPause = true);
        EventManager.OnResumeGame.AddListener(()=> isPause = false);
    }
    
    
    private void Start()
    {
        _camera = Camera.main;
       // _characterController = GetComponent<CharacterController>();
        //defaultCharacterRadius = _characterController.radius;
        cursor.SetActive(true);
    }

    void Update()
    {
        if (DialoguePrinter.instance.isDialogueCantInteract || isPause)
        {
            pointerImage.sprite = crosshair;
            return;
        }
           
        RaycastHit hit;
        Vector3 fwd = _camera.transform.TransformDirection(Vector3.forward);

        if (heldObject != null && Input.GetMouseButtonDown(0)) //drop object
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<InterfaceObject>().isDropped(fwd * impulseForce, 1, 4f);
            heldObject = null;
           // _characterController.radius = defaultCharacterRadius;
            cursor.SetActive(true);
        }
        else if (heldObject == null &&
                 Physics.Raycast(_camera.transform.position, fwd, out hit, grabDistance, layerMaskInteract.value))
        {
            pointerImage.sprite = hand;
            if (Input.GetMouseButtonDown(0)) //take object
            {
                heldObject = hit.collider.gameObject;
                var heldObjectInterface = heldObject.GetComponent<InterfaceObject>();
                if (heldObjectInterface.isBought) 
                {
                   TakeObject(heldObjectInterface);
                }
                else
                {
                    GameManager.Instance.BuyObject(heldObjectInterface);
                    if (heldObjectInterface.isBought) 
                    {
                        TakeObject(heldObjectInterface);
                    }
                    else
                    {
                        heldObject = null;
                    }
                }
            }
        }
        else
        {
            pointerImage.sprite = crosshair;
        }
    }

    public void TakeObject(InterfaceObject heldObjectInterface)
    {
        if (heldObjectInterface.isCam)
        {
            GameManager.Instance.FlyCameraBought();
            heldObjectInterface.DeleteInterface(true);
            return;
        }
        
        if (heldObjectInterface.CanTake)
        {
            heldObjectInterface.IsTaken();
            EventManager.OnItemHeld.Invoke();
            heldObject.transform.parent = heldObjectLocalPosition;
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = quaternion.identity;
            // _characterController.radius = 1f;
            cursor.SetActive(false);
        }

       
    }
}