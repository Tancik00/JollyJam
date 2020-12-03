using UnityEngine;
using UnityEngine.UI;

public class GameTimeController : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject timerPanel;
    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    
    public GameObject emptyStar;
    public GameObject emptyStar1;
    public GameObject emptyStar2;

    public Text timeText;
    public Text timeText1;
    public Text timeText2;

    public Slider slider;
    
    public static int passedTime;
    
    public static int timeLeft = 0;
    public static int time;

    private LevelsScriptData _levelsScriptData;
 
    void Start ()
    {
        _levelsScriptData = LevelsScriptData.GetInstance();
        if (LevelsScriptData.GetInstance().isTimeLimited)
        {
            timerPanel.SetActive(true);
            timeLeft = _levelsScriptData.timeForOneStar;
            time = 99;
            timeText.text = timeLeft.ToString();
            timeText1.text = time.ToString();

            InvokeRepeating("StartTimer", 1, 1);
            InvokeRepeating("StartCountingMilliseconds", 1, 0.01f);

            slider.maxValue = timeLeft;
            slider.minValue = 0f;
            
            SetStarPosition();
            
            emptyStar.transform.position = star.transform.position;
            emptyStar1.transform.position = star1.transform.position;
            emptyStar2.transform.position = star2.transform.position;
            
            timeText2.gameObject.SetActive(true);
        }
    }

    private void SetStarPosition()
    {
        var starPos = star.GetComponent<RectTransform>().anchoredPosition;
        
        var newPos = 900f - (float) _levelsScriptData.timeForThreeStars /
                     _levelsScriptData.timeForOneStar * 900f;
        star2.GetComponent<RectTransform>().anchoredPosition = new Vector2(newPos, starPos.y);
        
        newPos = 900f - (float) _levelsScriptData.timeForTwoStars /
                 _levelsScriptData.timeForOneStar * 900f;
        star1.GetComponent<RectTransform>().anchoredPosition = new Vector2(newPos, starPos.y);
    }

    private void StartTimer()
    {
        if (!AppStateData.IsPause)
        {
            if (timeLeft > 0) 
            {
                timeLeft--;
                timeText.text = timeLeft.ToString();
            }
        }
    }

    private void StartCountingMilliseconds()
    {
        if (!AppStateData.IsPause)
        {
            time--;
            timeText1.text = time.ToString();

            if (time <= 0)
            {
                time = 99;
            }
        }
    }

    private void Update()
    {
        if (_levelsScriptData.isTimeLimited)
        {
            slider.value = timeLeft;

            if (timeLeft == 0 && time==1)
            {
                timeText1.text = "0";
                AppStateData.IsPause = true;
                endPanel.SetActive(true);
            }

            passedTime = _levelsScriptData.timeForOneStar - timeLeft;

            if (BallPhysics.countOfBox > 0)
            {
                if (passedTime >= _levelsScriptData.timeForThreeStars)
                {
                    MakeStarNonActive(star2, emptyStar2);
                }

                if (passedTime >= _levelsScriptData.timeForTwoStars)
                {
                    MakeStarNonActive(star1, emptyStar1);
                }

                if (passedTime >= _levelsScriptData.timeForOneStar && time==1)
                {
                    MakeStarNonActive(star, emptyStar);
                }
            }
        }
    }

    private void MakeStarNonActive(GameObject star, GameObject emptyStar)
    {
        star.SetActive(false);
        emptyStar.SetActive(true);
    }
}
