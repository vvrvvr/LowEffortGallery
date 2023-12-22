using UnityEngine;

public class Coin : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log("collected");
         GameManager.Instance.IncreaseCoins();
         Destroy(gameObject);
      }
   }
}
