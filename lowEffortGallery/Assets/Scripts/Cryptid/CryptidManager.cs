using UnityEngine;

public class CryptidManager : MonoBehaviour
{
    public GameObject[] cryptids; // Массив объектов Cryptid
    private int activeCryptidIndex; // Индекс активного Cryptid

    // Вызывается при запуске сцены
    void Start()
    {
        // Проверяем, что массив Cryptids не пуст
        if (cryptids.Length > 0)
        {
            // Выключаем все Cryptids
            DisableAllCryptids();

            // Выбираем случайный индекс активного Cryptid
            activeCryptidIndex = Random.Range(0, cryptids.Length);

            // Включаем только выбранный Cryptid
            cryptids[activeCryptidIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Cryptids array is empty!");
        }
    }

    // Выключение всех Cryptids в массиве
    void DisableAllCryptids()
    {
        foreach (GameObject cryptid in cryptids)
        {
            cryptid.SetActive(false);
        }
    }
}