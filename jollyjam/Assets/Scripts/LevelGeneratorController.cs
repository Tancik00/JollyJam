using System.Collections;
using CloudOnce;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelGeneratorController : MonoBehaviour
{
    #region Public variables

    public GameObject ball;
    public static int levelLength;
    //public static int localCurrentLevel = -1;
    //public static float gameTime;
    public MainConfigScriptableObj mainConfigScriptableObject;
    [FormerlySerializedAs("uiManager")] public GamePlayUIManager gamePlayUiManager;
    public GameObject nextLevelPanel;
    public Text completeLevelText;
    public Text countOfSwipe;
    public ParticleSystem finishAnimationParticle;
    //public GameObject starPref;
    public GameObject enemyPref;
    [HideInInspector] public GameObject level;
    public SpriteRenderer backgroundSprite;

    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    public GameObject emptyStar;
    public GameObject emptyStar1;
    public GameObject emptyStar2;
    
    /*public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("level", 0);
        set => PlayerPrefs.SetInt("level", value);
    }*/
    
    public static int TotalStarCount
    {
        get => PlayerPrefs.GetInt("star", 0);
        set => PlayerPrefs.SetInt("star", value);
    }
    
    #endregion

    #region Private variables

    private GameObject[] _levels;
    private Vector3 spritePos2;
    private int childCount;
    private Transform tr;
    private static LevelGeneratorController _instance;
    private static Vector3 _spawnPos;
    private static Vector3 _botSpawnPos;
    private int time = 20;
    private int skinID;
    private int starCount;

    public static LevelsScriptData levelData;
    //private Vector3 starPos;

    #endregion

    public static LevelGeneratorController GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }

        return _instance;
    }

    public int LevelCount => _levels.Length;

    //private LevelProgressionData _levelProgressionData;

    private void Awake()
    {
        GameAnalytics.Initialize();
        // _levelProgressionData = LevelProgressionData.Instance;
        _instance = this;

        _levels = mainConfigScriptableObject.levels;
        if (LevelManager.localCurrentLevel == -1)
        {
            LevelManager.localCurrentLevel = CloudVariables.LastLevel;
        }

        var currentLevel = LevelManager.localCurrentLevel;
        if (currentLevel >= _levels.Length)
        {
            currentLevel = _levels.Length - 1;
        }

        level = _levels[currentLevel];
        childCount = level.transform.childCount;
        tr = level.transform;

        levelData = LevelsScriptData.GetInstance();

        BallPhysics.countOfBox = 0;

        SetBackground();

        Instantiate(_levels[currentLevel], Vector3.zero, Quaternion.identity);

        AdaptCameraSizeToLevel();

        levelData = _levels[currentLevel].GetComponent<LevelsScriptData>();

        GetBoxCount();

        GetPlayerSpawnPosition();

        Instantiate(ball, _spawnPos, Quaternion.identity);

        GetBotSpawnPositionAndInstantiateIt(levelData);

        SortObjectsByZ();

        GenerateDecor();

        levelLength = _levels.Length;
    }

    private void Start()
    {
        MovableBallController.count = 0;
        GameTimeController.passedTime = 0;
        SwipeCountPanel.swipeCount = 0;

        skinID = level.GetComponent<LevelsScriptData>().skinID;
    }

    private void SetBackground()
    {
        backgroundSprite.sprite = mainConfigScriptableObject.skins[skinID].background;
    }

    private void GetBoxCount()
    {
        for (int i = 0; i < childCount; i++)
        {
            if (tr.GetChild(i).CompareTag("box"))
            {
                BallPhysics.countOfBox++;
            }
        }
    }

    private void GetPlayerSpawnPosition()
    {
        for (int i = 0; i < childCount; i++)
        {
            if (tr.GetChild(i).CompareTag("spawn"))
            {
                _spawnPos = tr.GetChild(i).position;
            }
        }
    }

    private void GetBotSpawnPositionAndInstantiateIt(LevelsScriptData levelsScriptData)
    {
        for (int i = 0; i < childCount; i++)
        {
            if (tr.GetChild(i).CompareTag("botSpawn"))
            {
                _botSpawnPos = tr.GetChild(i).position;
                if (levelsScriptData.isBotSet)
                {
                    Instantiate(enemyPref, _botSpawnPos, Quaternion.identity);
                }
            }
        }
    }

    private void SortObjectsByZ()
    {
        for (int i = 0; i < childCount; i++)
        {
            //if (tr.GetChild(i).CompareTag("obstacle") || tr.GetChild(i).CompareTag("changingBox"))
            //{
            Vector3 newPos = tr.GetChild(i).position;
            newPos.z = newPos.y * 0.1f;
            tr.GetChild(i).position = newPos;
            //}
        }
    }

    private void GenerateDecor()
    {
        for (int i = 0; i < childCount; i += Random.Range(6, 9))
        {
            var obs = tr.GetChild(i);
            if (obs.CompareTag("obstacle"))
            {
                Instantiate(mainConfigScriptableObject.skins[skinID].decor[Random.Range(0, 5)], obs.position,
                    Quaternion.identity);
            }
        }
    }

    private void AdaptCameraSizeToLevel()
    {
        for (int i = 0; i < childCount; i++)
        {
            if (tr.GetChild(i).name == "Border")
            {
                spritePos2 = tr.GetChild(i).GetComponent<SpriteRenderer>().bounds.max;
            }
        }
        
        var pos2 = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Camera.main.orthographicSize *= (spritePos2.x / pos2.x);
    }

    /*private void Update()
    {
        gameTime = Time.timeSinceLevelLoad;
    }*/

    public void FinishGame()
    {
       gamePlayUiManager.MakeButtonsInactive();
        StartCoroutine(FinishAnimation());
        completeLevelText.text = "Game complete!";
    }
    public void FinishLevel()
    {
        //play finish animation
        gamePlayUiManager.MakeButtonsInactive();
        StartCoroutine(FinishAnimation());
        completeLevelText.text = $"Level {LevelManager.localCurrentLevel} complete!";
        //Debug.Log("***" + gameTime);

        Cloud.Storage.Save();
    }

    public IEnumerator FinishAnimation()
    {
        finishAnimationParticle.gameObject.SetActive(true);

        Transform particleTransform = finishAnimationParticle.transform;
        for (int i = 0; i < childCount; i++)
        {
            var currentChildTransform = tr.GetChild(i);
            if (currentChildTransform.CompareTag("box"))
            {
                particleTransform.position = currentChildTransform.position;
                finishAnimationParticle.Emit(50);
                yield return new WaitForSeconds(0.005f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        nextLevelPanel.SetActive(true);

        if (levelData.isTimeLimited)
        {
            ShowStarCountDependingOnTheTime();
        }
        else
        {
            ShowStarCount();   
        }
        
        yield return null;
    }

    private void ShowStarCount()
    {
        //starPos = countOfSwipe.transform.position;
        if (MovableBallController.count <= levelData.min)
        {
            SetStarCount(3);
        }
        else if (MovableBallController.count <= levelData.medium)
        {
            SetStarCount(2);
        }
        else if (MovableBallController.count > levelData.medium)
        {
            SetStarCount(1);
        }

        countOfSwipe.text = $"Count of swipe: {MovableBallController.count}\nCount of star: {starCount}";
    }

    private void ShowStarCountDependingOnTheTime()
    {
        if (GameTimeController.passedTime < levelData.timeForThreeStars)
        {
            SetStarCount(3);
        }
        else if (GameTimeController.passedTime < levelData.timeForTwoStars)
        {
            SetStarCount(2);
        }
        
        else if (GameTimeController.passedTime <= levelData.timeForOneStar)
        {
            SetStarCount(1);
        }
    }

    private void SetStarCount(int count)
    {
        LevelProgressionData.Instance.UpdateLevelDataAndSave(LevelManager.localCurrentLevel-1, count);

        starCount = count;

        if (count == 1)
        {
            emptyStar.SetActive(false);
            star.SetActive(true);
        }
        else if (count == 2)
        {
            emptyStar.SetActive(false);
            star.SetActive(true);
            emptyStar1.SetActive(false);
            star1.SetActive(true);
        }
        else if (count == 3)
        {
            emptyStar.SetActive(false);
            star.SetActive(true);
            emptyStar1.SetActive(false);
            star1.SetActive(true);
            emptyStar2.SetActive(false);
            star2.SetActive(true);
        }
        /*for (int i = 0; i < count; i++)
        {
            Instantiate(starPref, new Vector3(starPos.x, starPos.y, starPos.z), Quaternion.identity);
            starPos.x += 0.5f;
        }*/

        TotalStarCount += count;
    }
}
