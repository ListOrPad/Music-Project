using UnityEngine;

public class Track : MonoBehaviour
{
    public bool Completed { get; set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }



}
