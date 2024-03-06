using UnityEngine;

public class SignArea : MonoBehaviour
{
    public GameObject Sign;

    private void Start()
    {
        Sign.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Sign.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Sign.SetActive(false);
        }
    }
}
