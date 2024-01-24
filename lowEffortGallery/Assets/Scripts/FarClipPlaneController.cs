using UnityEngine;
using Cinemachine;

public class FarClipPlaneController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float clipPlaneSpeed = 1.0f;
    public float farClipMax = 100.0f;

    private bool isFarClipMaxReached = false;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera не присвоен!");
            return;
        }

        // Устанавливаем far clip plane в начальное значение (ноль)
        virtualCamera.m_Lens.FarClipPlane = 0.0f;
    }

    void Update()
    {
        if (!isFarClipMaxReached)
        {
            // Увеличиваем значение far clip plane по мере времени
            virtualCamera.m_Lens.FarClipPlane = Mathf.MoveTowards(
                virtualCamera.m_Lens.FarClipPlane,
                farClipMax,
                clipPlaneSpeed * Time.deltaTime
            );

            // Проверяем, достигли ли максимального значения
            if (Mathf.Approximately(virtualCamera.m_Lens.FarClipPlane, farClipMax))
            {
                isFarClipMaxReached = true;
                // Выполните здесь необходимые действия после достижения максимального значения
                // Например, отключите скрипт или выполните другие действия
                // gameObject.GetComponent<FarClipPlaneController>().enabled = false;
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}