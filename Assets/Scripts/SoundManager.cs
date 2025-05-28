using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource Source { get; set; }

    [DllImport("__Internal")]
    public static extern void InitializeAudioSystem(IntPtr data, int length);

    [DllImport("__Internal")]
    public static extern void ControlAudio(string action);

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        //Keep this object even when we go to a new scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy duplicate gameobjects
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResumeTrack()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ControlAudio("play");
#else
        Source.Play();
#endif
    }
    public void PauseTrack()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ControlAudio("pause");
#else
        Source.Pause();
#endif
    }

    public void ResetProgress(ProgressBar progressBar)
    {
        progressBar.ProgressSlider.value = 0;
        progressBar.progressText.text = "0%";
        Source.clip = null;
        Source.clip = TrackList.CurrentTrack.Clip;
    }

    public void ChangeSpeed(int speed)
    {
        Game.ClipSpeed = speed;
        if (Game.ClipSpeed == (int)AudioSpeed.Normal || Game.ClipSpeed == (int)AudioSpeed.Auto)
        {
            Source.pitch = 1f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Fast)
        {
            Source.pitch = 1.5f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Faster)
        {
            Source.pitch = 2f;
        }
        else if (Game.ClipSpeed == (int)AudioSpeed.Fastest)
        {
            Source.pitch = 3f;
        }
        
    }

    public static void InitializeWebGLAudio(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        byte[] byteData = new byte[samples.Length * 4];
        Buffer.BlockCopy(samples, 0, byteData, 0, byteData.Length);

        IntPtr ptr = Marshal.AllocHGlobal(byteData.Length);
        Marshal.Copy(byteData, 0, ptr, byteData.Length);

        InitializeAudioSystem(ptr, byteData.Length);
        Marshal.FreeHGlobal(ptr);
    }


}