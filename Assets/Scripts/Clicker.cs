using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    //pic section
    [field:SerializeField] public GameObject ClickerPic { get; set; }
    public Sprite Pic {get; set; }
    private void Twitch()
    {
        gameObject.transform.localPosition = new Vector2(10, 10);

    }

    public void Click(Game game)
    {
        if (!game.AudSource.isPlaying)
        {
            SoundManager.Instance.ResumeTrack();
        }
        if (game.AudSource.isPlaying)
        {
            game.BgAnimation.StartAnimation();
            game.Timer = 0; //reset timer
        }
        //Twitch();
    }
}
