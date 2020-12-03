using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.UI;

public class LevelBtnView : MonoBehaviour
{
    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    
    public Image ButtonImage;
    
    public Button Button;
    
    public Sprite Open;
    public Sprite Close;
    
    public Text Text;

    private GamePlayUIManager _manager;
    private int _levelNum;
    private int count;

    public void SelectLevel()
    {
        _manager.SelectLevel(_levelNum);
        GameAnalytics.NewDesignEvent("ButtonClick:LevelsMap ", _levelNum);
    }

    public void SetUp(GamePlayUIManager gamePlayUiManager, int levelNum, bool isOpen)
    {
        _manager = gamePlayUiManager;
        _levelNum = levelNum;
        Text.text = (levelNum + 1).ToString();
        if (isOpen)
        {
            ButtonImage.sprite = Open;
            Button.interactable = true;
        }
        else
        {
            ButtonImage.sprite = Close;
            Button.interactable = false;
        }

        count = LevelProgressionData.Instance.GetStars(_levelNum);

        if (count == 1)
        {
            star.SetActive(true);
        }
        else if (count == 2)
        {
            star1.SetActive(true);
        }
        else if (count == 3)
        {
            star2.SetActive(true);
        }
    }
}
