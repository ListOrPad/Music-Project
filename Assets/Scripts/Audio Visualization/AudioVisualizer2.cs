using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer2 : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform[] visualizerObjects; // Объекты для визуализации
    public float scaleMultiplier = 10f; // Множитель для масштабирования

    //Smoothing
    private float[] smoothedAmplitudes = new float[8];
    public float smoothSpeed = 0.5f; // Скорость сглаживания

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float[] samples = new float[1024];
        audioSource.clip.GetData(samples, audioSource.timeSamples);

        // Разделяем samples на восемь частей
        int samplesPerObject = samples.Length / 8;

        for (int i = 0; i < 8; i++)
        {
            float sum = 0;

            // Вычисляем среднюю амплитуду для каждой части
            for (int j = 0; j < samplesPerObject; j++)
            {
                int index = i * samplesPerObject + j;
                sum += Mathf.Abs(samples[index]);
            }

            float averageAmplitude = sum / samplesPerObject;

            // Сглаживаем амплитуду
            smoothedAmplitudes[i] = Mathf.Lerp(smoothedAmplitudes[i], averageAmplitude, smoothSpeed * Time.deltaTime);

            // Масштабируем объект
            visualizerObjects[i].localScale = new Vector3(1, smoothedAmplitudes[i] * scaleMultiplier, 1);
        }
    }
}
