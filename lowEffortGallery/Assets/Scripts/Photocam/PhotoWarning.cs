using UnityEngine;

public class PhotoWarning : MonoBehaviour
{
    public GameObject photoWarning;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photoWarning.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photoWarning.SetActive(false);
        }
    }

    public void DisableWarning()
    {
        photoWarning.SetActive(false);
        Destroy(gameObject);
    }
}
