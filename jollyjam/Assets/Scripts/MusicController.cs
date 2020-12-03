using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite musicIconOn;
    public Sprite musicIconOff;
    public Image musicBtn;

    void Update()
    {
        if (AppStateData.isMusicOn)
        {
            //hitSound.mute = false;
            musicBtn.sprite = musicIconOn;
        }
        else
        {
            //hitSound.mute = true;
            musicBtn.sprite = musicIconOff;
        }
    }
}
