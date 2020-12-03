using UnityEngine;
using UnityEngine.UI;

public class TimeLevelInfo : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = "Уровень необходимо пройти за " + LevelsScriptData.GetInstance().timeForOneStar + " секунд!";
    }
    
    public void ClosePopup()
    {
        Destroy(gameObject);
        AppStateData.IsPause = false;
    }
}
