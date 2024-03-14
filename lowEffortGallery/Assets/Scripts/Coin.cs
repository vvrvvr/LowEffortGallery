using UnityEngine;

public class Coin : MonoBehaviour
{
   public AudioSource _Audio;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         GameManager.Instance.IncreaseCoins();
         Destroy(gameObject);
      }
   }
}
