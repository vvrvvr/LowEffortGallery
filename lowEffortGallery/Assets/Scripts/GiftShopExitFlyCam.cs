using UnityEngine;
using DG.Tweening;

public class GiftShopExitFlyCam : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float _portalSpeed = 1f;
    [SerializeField] private Transform _edgePortal;

    //private vars
    private Transform anchor;
    public Transform _player;

    private void Start()
    {
        anchor = GetComponent<Transform>();
        //_player = ThirdPersonController.Instance;
    }

    private void Update()
    {
        ControlPortalPosition();
    }

    private void ControlPortalPosition()
    {
        var position = anchor.position;
        Vector3 playerPos = _player.transform.position;

        // Calculate the vector from anchor to player, considering y-coordinate
        Vector3 anchorToPlayer = playerPos - position;

        float distanceToPlayer = anchorToPlayer.magnitude;

        if (distanceToPlayer > distance)
        {
            // Calculate the new position considering y-coordinate
            Vector3 newPos = position + anchorToPlayer.normalized * distance;
            _edgePortal.position = newPos;
        }
        else
        {
            // Calculate the new position considering y-coordinate
            Vector3 newAnchorToObj = anchorToPlayer.normalized * distance;
            Vector3 newPos = position + newAnchorToObj;

            // Use DOTween to smoothly move the _edgePortal to the new position
            _edgePortal.DOMove(newPos, _portalSpeed).SetEase(Ease.Linear);
        }
    }
}