using System;
using UnityEngine;
using System.Collections;


public class CryptidController : MonoBehaviour
{
    public GameObject cryptidObject; // Ссылка на объект Cryptid
    public float showTime = 0.1f; // Время отображения Cryptid в секундах
    private bool isOnce = true;

    private void Awake()
    {
        // Убедимся, что объект Cryptid установлен
        if (cryptidObject == null)
        {
            Debug.LogError("Cryptid object is not assigned!");
        }
        cryptidObject.SetActive(false);
        
        // MeshRenderer currentMeshRenderer = GetComponent<MeshRenderer>();
        // if (currentMeshRenderer != null)
        // {
        //     currentMeshRenderer.enabled = false;
        // }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnce)
        {
            Debug.Log("cryptid shown");
            isOnce = false;
            ShowCryptid();
        }
    }

    // Метод для отображения Cryptid на заданное время
    public void ShowCryptid()
    {
        // Проверяем, что объект Cryptid установлен
        if (cryptidObject != null)
        {
            // Включаем объект Cryptid
            cryptidObject.SetActive(true);

            // Запускаем корутину для отключения объекта через заданное время
            StartCoroutine(HideCryptidAfterDelay());
        }
        else
        {
            Debug.LogError("Cryptid object is not assigned!");
        }
    }
    
    
    // Корутина для отключения Cryptid через заданное время
    IEnumerator HideCryptidAfterDelay()
    {
        // Ждем заданное количество секунд
        yield return new WaitForSeconds(showTime);

        // Выключаем объект Cryptid
        cryptidObject.SetActive(false);
    }
}