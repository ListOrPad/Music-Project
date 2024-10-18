using UnityEngine;

public class Track : MonoBehaviour
{
    public bool UniqueCompleted { get; set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }



}
