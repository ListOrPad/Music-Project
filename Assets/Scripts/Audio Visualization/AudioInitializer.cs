using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class AudioInitializer : MonoBehaviour
{
    [SerializeField] private GameObject audioInitButton;

    [DllImport("__Internal")]
    private static extern void EnableAudioSystem();

    public void OnAudioInitClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        EnableAudioSystem();
#endif
        audioInitButton.SetActive(false);
    }
}
