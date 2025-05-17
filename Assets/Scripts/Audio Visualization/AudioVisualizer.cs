using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[256]; // 256, cause fftSize/2 = 512/2
    public static float[] freqBand = new float[8];
    public static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    // Import Javascript functions
    [DllImport("__Internal")]
    private static extern void InitializeAudioAnalyzer();

    [DllImport("__Internal")]
    private static extern string GetSpectrumData();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
#if UNITY_WEBGL && !UNITY_EDITOR
        InitializeAudioAnalyzer();
#endif
        StartCoroutine(UpdateVisualization());
    }

    private IEnumerator UpdateVisualization()
    {
        while (true)
        {
            Debug.Log("Updating Visualisation");
#if UNITY_WEBGL && !UNITY_EDITOR
            GetSpectrumFromWebGL(); //should work in web
#else
            GetSpectrumAudioSource(); // works in editor
#endif
            MakeFrequencyBands();
            BandBuff();
            yield return new WaitForSeconds(1f / 30f); // 30 times/sec
        }
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        Debug.Log("Spectrum 0: " + samples[0] + ", Spectrum 1: " + samples[1]);
    }

    private void GetSpectrumFromWebGL()
    {
        string data = GetSpectrumData();
        if (!string.IsNullOrEmpty(data))
        {
            string[] values = data.Split(',');
            for (int i = 0; i < Mathf.Min(values.Length, samples.Length); i++)
            {
                samples[i] = float.Parse(values[i]) / 255f; // normalize from 0-255 to 0-1
            }
            Debug.Log("WebGL Spectrum 0: " + samples[0] + ", Spectrum 1: " + samples[1]);
        }
    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = 4;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (count < samples.Length)
                {
                    average += samples[count] * (count + 1);
                    count++;
                }
            }

            average /= sampleCount;
            freqBand[i] = average * 10;
        }
    }

    private void BandBuff()
    {
        for (int i = 0; i < 8; ++i)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.1f;
            }
            if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i] * Time.deltaTime * 340;
                bufferDecrease[i] *= 1.05f;
            }
        }
    }
}