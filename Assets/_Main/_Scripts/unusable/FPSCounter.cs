using UnityEngine;
using TMPro; // Необходимо для работы с TextMeshPro

// THIS script was made by AI, bc i'll delete this
// Its just debug feauture

public class FPSCounter : MonoBehaviour
{
    // Ссылка на компонент TextMeshPro, который будет отображать FPS
    [SerializeField] private TextMeshProUGUI fpsText;

    // Интервал, через который мы обновляем текст (для экономии ресурсов)
    private float refreshRate = 0.5f;
    private float timer = 0f;
    private int frameCount = 0;

    void Update()
    {
        // 1. Считаем количество кадров за интервал refreshRate
        timer += Time.unscaledDeltaTime; // Используем unscaledDeltaTime, чтобы FPS не зависел от паузы в игре
        frameCount++;

        // 2. Проверяем, прошло ли достаточно времени для обновления
        if (timer >= refreshRate)
        {
            // Расчет FPS: FPS = Количество кадров / Прошедшее время
            float currentFPS = frameCount / timer;

            // Форматируем текст (округляем до целого числа)
            string text = string.Format("{0:0} FPS", currentFPS);

            // Обновляем UI
            if (fpsText != null)
            {
                fpsText.text = text;
            }

            // Сбрасываем счетчики для следующего интервала
            timer = 0f;
            frameCount = 0;
        }
    }

    // Дополнительно: Проверка, что ссылка на текст установлена
    void Awake()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPS TextMeshProUGUI component is not assigned in the inspector!");
        }
    }
}
