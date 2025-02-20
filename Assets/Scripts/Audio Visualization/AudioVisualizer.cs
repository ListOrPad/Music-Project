//using System.Collections;
//using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//public class AudioVisualizer : MonoBehaviour
//{
//    AudioSource audioSource;
//    public static float[] samples = new float[512];
//    public static float[] freqBand = new float[8];
//    public static float[] bandBuffer = new float[8];

//    private float[] bufferDecrease = new float[8];

//    private void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        StartCoroutine(UpdateVisualization()); // Используем корутину
//    }

//    private IEnumerator UpdateVisualization()
//    {
//        while (true)
//        {
//            Debug.Log("Updating Visualisation");
//            GetSpectrumAudioSource();
//            MakeFrequencyBands();
//            BandBuff();
//            yield return new WaitForSeconds(1f / 30f); // 30 раз в секунду
//        }
//    }

//    private void GetSpectrumAudioSource()
//    {
//        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

//        Debug.Log("Spectrum 0: " + samples[0] + ", Spectrum 1: " + samples[1]);
//    }

//    private void MakeFrequencyBands()
//    {
//        int count = 0;

//        for (int i = 0; i < 8; i++)
//        {
//            float average = 0;
//            int sampleCount = 4;

//            if (i == 7)
//            {
//                sampleCount += 2;
//            }

//            for (int j = 0; j < sampleCount; j++)
//            {
//                average += samples[count] * (count + 1);
//                count++;
//            }

//            average /= sampleCount;

//            freqBand[i] = average * 10;
//        }
//    }

//    private void BandBuff()
//    {
//        for (int i = 0; i < 8; ++i)
//        {
//            if (freqBand[i] > bandBuffer[i])
//            {
//                bandBuffer[i] = freqBand[i];
//                bufferDecrease[i] = 0.1f;
//            }
//            if (freqBand[i] < bandBuffer[i])
//            {
//                bandBuffer[i] -= bufferDecrease[i] * Time.deltaTime * 100; // тут потом изменить на *340
//                bufferDecrease[i] *= 1.05f;
//            }
//        }
//    }
//}