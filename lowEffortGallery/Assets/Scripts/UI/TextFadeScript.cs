using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class TextFadeScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float delay = 2f; // Задержка перед началом анимации
    public float fadeTime = 1f; // Время затухания

    private void Start()
    {
        // Запускаем корутину, чтобы дождаться заданной задержки
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(delay);

        // Запускаем анимацию затухания
        FadeText();
    }

    private void FadeText()
    {
        // Начальный цвет (255 - полная видимость)
        Color startColor = textMesh.color;

        // Конечный цвет (0 - полная прозрачность)
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Используем DOTween для анимации затухания
        textMesh.DOColor(endColor, fadeTime)
            .OnComplete(() => DisableScript()); // Вызываем метод по завершении анимации
    }

    private void DisableScript()
    {
        // Отключаем этот скрипт
        GameManager.Instance.fadeSpeed = 1f;
        enabled = false;
    }
}