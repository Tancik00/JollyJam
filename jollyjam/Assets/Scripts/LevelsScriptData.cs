using UnityEngine;

public class LevelsScriptData : MonoBehaviour
{
    public int min;
    public int medium;
    public int max;
    
    public int timeForOneStar;
    public int timeForTwoStars;
    public int timeForThreeStars;

    public int skinID = 0;
    public bool isTimeLimited;
    public bool isBotSet;

    public Transform playerSpawn;
    
    private static LevelsScriptData _instance;

    public static LevelsScriptData GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }

        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
