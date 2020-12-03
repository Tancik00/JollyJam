using UnityEngine;
using UnityEngine.UI;

public class SimpleLevelInfo : MonoBehaviour
{
    public Text text;
    private void Start()
    {
        text.text = "Уровень необходимо пройти за " + LevelsScriptData.GetInstance().max + " ходов!";
    }

    public void ClosePopup()
    {
        Destroy(gameObject);
        AppStateData.IsPause = false;
    }
}
