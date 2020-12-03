using System.Collections.Generic;
using UnityEngine;
using CloudOnce;
public class BallPhysics : MonoBehaviour
{
    #region Public variables

    public static int countOfBox;
    public PlayerConfigScriptableObject playerConfig;
    public MainConfigScriptableObj mainConfigScriptableObj;
    public ParticleSystem anim;
    #endregion

    private bool _isChecked;
    private ParticleSystem destroyedEnemyAnimation;
    private Sprite filledSprite;
    private Sprite changedSprite;

    private void Start()
    {
        var skinID = LevelGeneratorController.GetInstance().level.GetComponent<LevelsScriptData>().skinID;
        //filledSprite = mainConfigScriptableObj.skins[skinID].filledBox;
        changedSprite = mainConfigScriptableObj.skins[skinID].changedBox;
    }

    private void Update()
    {
        if (_isChecked)
        {
            countOfBox--;

            if (LevelsScriptData.GetInstance().isTimeLimited)
            {
                if (countOfBox <= 0 && GameTimeController.time > 0)
                {
                    FinishLevel();
                }
            }
            else
            {
                if (countOfBox <= 0)
                {
                    FinishLevel();
                }
            }

            _isChecked = false;
        }
    }

    private void FinishLevel()
    {
        AppStateData.IsPause = true;
        if (LevelGeneratorController.levelLength <= LevelManager.localCurrentLevel + 1)
        {
            LevelGeneratorController.GetInstance().FinishGame();
#if UNITY_EDITOR
            Debug.Log("** Game Finished **");
#endif
            LevelManager.localCurrentLevel++;
            if (LevelManager.localCurrentLevel > CloudVariables.LastLevel)
            {
                CloudVariables.LastLevel = LevelManager.localCurrentLevel;
            }
        }
        else
        {
            LevelManager.localCurrentLevel++;
            if (LevelManager.localCurrentLevel > CloudVariables.LastLevel)
            {
                CloudVariables.LastLevel = LevelManager.localCurrentLevel;
            }

            LevelGeneratorController.GetInstance().FinishLevel();
            UnlockFirstCreamAchievement();
/*#if UNITY_EDITOR
            Debug.Log(LevelGeneratorController.gameTime);
#endif
            GameAnalytics.NewDesignEvent(
                "LevelProgression:Complete: " + (LevelManager.localCurrentLevel + 1),
                LevelGeneratorController.gameTime);
            SendEvent(LevelManager.localCurrentLevel, LevelGeneratorController.gameTime);
            LevelGeneratorController.gameTime = 0;*/
        }
    }

    private void UnlockFirstCreamAchievement()
    {
        if (CloudVariables.LastLevel == 1)
        {
            Achievements.FirstCream.Unlock();
#if UNITY_EDITOR
            Debug.Log("Unlock FirstCream");
#endif
        }
    }

    void SendEvent(int levelCount, float time)
    {
        AnalyticsHandler.SendAnalyticsEvent("LevelFinished", new Dictionary<string, object>
        {
            {"Level_" + levelCount, time}
        });
#if UNITY_EDITOR
        Debug.Log("Event Registered from BallPhysics - Level_" + levelCount + " time = " + time);
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var checker = other.GetComponent<Checker>();
        if (other.CompareTag("box"))
        {
            //checker.UpdateTile();
            //other.GetComponent<SpriteRenderer>().sprite = filledSprite;

            if ((checker.isChecked != false))
                return;

            _isChecked = true;
            checker.isChecked = true;
        }

        if (other.gameObject.CompareTag("bot"))
        {
            destroyedEnemyAnimation = Instantiate(anim, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            destroyedEnemyAnimation.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("changingBox"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Obstacle");
            other.GetComponent<SpriteRenderer>().sprite = changedSprite;
        }
    }
}
