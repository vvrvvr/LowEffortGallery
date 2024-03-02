using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftshopPlayerTeleportator : MonoBehaviour
{
    public Transform teleportSpot;
    public GameObject player;
    void Start()
    {
        // Находим игрока в сцене и сохраняем ссылку на его объект
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("teleport");
            player.SetActive(false);
            player.transform.position = teleportSpot.position;
            player.SetActive(true);
        }
    }
}
