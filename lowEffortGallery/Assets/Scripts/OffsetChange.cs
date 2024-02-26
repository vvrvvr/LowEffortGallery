using UnityEngine;

public class OffsetChange : MonoBehaviour
{
    public float speed = 0.1f; // Скорость изменения смещения
    private Renderer rend; // Ссылка на компонент Renderer
    private Material mat; // Ссылка на материал объекта
    private float currentOffsetX = 0f; // Текущее значение смещения по X

    void Start()
    {
        rend = GetComponent<Renderer>(); // Получаем компонент Renderer
        if (rend != null)
        {
            mat = rend.material; // Получаем материал объекта
        }
        else
        {
            Debug.LogError("Renderer component not found!");
            enabled = false; // Отключаем скрипт, если компонент Renderer не найден
        }
    }

    void Update()
    {
        // Увеличиваем значение смещения по X
        currentOffsetX += speed * Time.deltaTime;
        // Применяем новое значение смещения к материалу
        mat.SetTextureOffset("_BaseMap", new Vector2(currentOffsetX, 0f));
    }
}