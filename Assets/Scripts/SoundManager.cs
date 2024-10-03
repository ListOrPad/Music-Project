using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

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

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ChangeSpeed(int speed)
    {
        if (speed == (int)PlayMode.Normal || speed == (int)PlayMode.Auto)
        {
            source.pitch = 1f;
        }
        else if (speed == (int)PlayMode.Fast)
        {
            source.pitch = 1.5f;
        }
        else if (speed == (int)PlayMode.Faster)
        {
            source.pitch = 2f;
        }
        else if (speed == (int)PlayMode.Fastest)
        {
            source.pitch = 3f;
        }
    }
}