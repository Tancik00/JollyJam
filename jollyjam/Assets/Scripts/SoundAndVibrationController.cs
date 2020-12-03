using UnityEngine;
using UnityEngine.UI;

public class SoundAndVibrationController : MonoBehaviour
{
    public Sprite musicIconOn;
    public Sprite musicIconOff;
    public Sprite soundIconOn;
    public Sprite soundIconOff;
    
    public Image musicBtn;    
    public Image soundBtn;

    public Sprite vibrationIconOn;
    public Sprite vibrationIconOff;
    public Image vibrationBtn;

    private void Start()
    {
        /*if (AppStateData.isSoundOn)
        {
            //hitSound.mute = false;
            musicBtn.sprite = musicIconOn;
        }
        else
        {
            //hitSound.mute = true;
            musicBtn.sprite = musicIconOff;
        }*/
    }

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

        if (AppStateData.isSoundOn)
        {
            soundBtn.sprite = soundIconOn;
        }
        else
        {
            soundBtn.sprite = soundIconOff;
        }

        //vibrationBtn.sprite = AppStateData.isVibrationOn ? vibrationIconOn : vibrationIconOff;
    }
}
