using UnityEngine;

public class RotationLimiter : MonoBehaviour
{
    public AxisRotation player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isLimiter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isLimiter = false;
        }
    }
}
