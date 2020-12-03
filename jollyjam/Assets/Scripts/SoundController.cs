using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Sprite soundIconOn;
    public Sprite soundIconOff;
    public Image soundBtn;

    private void Update()
    {
        if (AppStateData.isSoundOn)
        {
            soundBtn.sprite = soundIconOn;
        }
        else
        {
            soundBtn.sprite = soundIconOff;
        }
    }
}
