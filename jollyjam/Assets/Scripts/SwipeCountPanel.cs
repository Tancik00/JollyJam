using UnityEngine;
using UnityEngine.UI;

public class SwipeCountPanel : MonoBehaviour
{
    public GameObject swipeCountPanel;
    public GameObject losePanel;
    public GameObject star;
    public GameObject star1;
    public GameObject star2; 
    
    public GameObject emptyStar;
    public GameObject emptyStar1;
    public GameObject emptyStar2;

    public Slider slider;
    
    public Text swipeCountText;
    
    public static int swipeCount;

    private LevelsScriptData _levelsScriptData;
    private int _countOfSwipe;
    private void Start()
    {
        _levelsScriptData = LevelsScriptData.GetInstance();
        _countOfSwipe = MovableBallController.count;
        if (!LevelsScriptData.GetInstance().isTimeLimited)
        {
            swipeCountPanel.SetActive(true);
            swipeCount = _levelsScriptData.max;
            swipeCountText.text = swipeCount.ToString();

            slider.maxValue = swipeCount;
            slider.minValue = 0f;

            SetStarPosition();
            emptyStar.transform.position = star.transform.position;
            emptyStar1.transform.position = star1.transform.position;
            emptyStar2.transform.position = star2.transform.position;
        }
    }

    private void SetStarPosition()
    {
        var starPos = star.GetComponent<RectTransform>().anchoredPosition;

        var newPos = 1000f - (float) _levelsScriptData.min / _levelsScriptData.max * 1000f;
        star2.GetComponent<RectTransform>().anchoredPosition=new Vector2(newPos, starPos.y);
        
        newPos = 1000f - (float) _levelsScriptData.medium / _levelsScriptData.max * 1000f;
        star1.GetComponent<RectTransform>().anchoredPosition=new Vector2(newPos, starPos.y);
    }

    private void Update()
    {
        if (!LevelsScriptData.GetInstance().isTimeLimited)
        {
            swipeCountText.text = swipeCount.ToString();

            slider.value = swipeCount;

            if (BallPhysics.countOfBox > 0)
            {
                if (MovableBallController.count >= _levelsScriptData.min)
                {
                    MakeStarsNonActive(star2, emptyStar2);
                }

                if (MovableBallController.count >= _levelsScriptData.medium)
                {
                    MakeStarsNonActive(star1, emptyStar1);
                }

                if (MovableBallController.count >= _levelsScriptData.max)
                {
                    MakeStarsNonActive(star, emptyStar);
                }
            
                if (swipeCount <= 0)
                {
                    AppStateData.IsPause = true;
                    losePanel.SetActive(true);
                }
            }
        }
    }

    private void MakeStarsNonActive(GameObject star, GameObject emptyStar)
    {
        star.SetActive(false);
        emptyStar.SetActive(true);
    }
}
