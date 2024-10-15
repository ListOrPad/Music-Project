using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [field:SerializeField] public Button ClickerButton { get; private set; }
    //pic section
    [field:SerializeField] public GameObject ClickerPic { get; set; }
    public Sprite Pic {get; set; }

    private void Twitch(Game game)
    {
        game.Anim.SetTrigger("Click");
        System.Random random = new System.Random();
        int randomState = random.Next(1, 3); //generates number from 1 to 2
        if(randomState == 1)
        {
            game.Anim.Play("TwitchLeft");
        }
        else
        {
            game.Anim.Play("TwitchRight");
        }
        game.Anim.ResetTrigger("Click");
    }

    public void Click(Game game)
    {
        if (!game.AudSource.isPlaying)
        {
            SoundManager.Instance.ResumeTrack();
        }
        if (game.AudSource.isPlaying)
        {
            Twitch(game);
            game.BgAnimation.StartAnimation();
            game.Timer = 0; //reset timer
        }
    }
}
