using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class CloudManager : MonoBehaviour
{
    //public Text text;
    
    private LevelProgressionData _levelProgressionData;
    void Start()
    {
        Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
        Cloud.OnCloudLoadComplete += CloudOnceLoadComplete;
        Cloud.Initialize(true, true);
    }

    private void CloudOnceInitializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
        Debug.Log("Initialized");
        Cloud.Storage.Load();
    }

     private void CloudOnceLoadComplete(bool success)
     {
         _levelProgressionData = LevelProgressionData.Instance;
         //text.text = CloudVariables.NewJson;
     }
 
     /*public void ButtonClick()
     {
     }*/
}

[System.Serializable]
public class LevelProgressionData
{
    public List<LevelData> _levelInfo;
    private static LevelProgressionData _instance;

    public static LevelProgressionData Instance
    {
        get
        {
            if (_instance == null)
                _instance = Load();
            return _instance;
        }
    }
    
    private static LevelProgressionData Load()
    {
        var json = string.Empty;
        
        if (string.IsNullOrEmpty(CloudVariables.NewJson))
        {
            json = PlayerPrefs.GetString("Stars", string.Empty);
        }
        else
        {
            json = CloudVariables.NewJson;
            json = json.Replace('|', '"');
        }

        var l = JsonUtility.FromJson<LevelProgressionData>(json);
            if (l == null)
            {
                l = new LevelProgressionData();
            }

            if (l._levelInfo == null)
                l._levelInfo = new List<LevelData>();
            return l;
            
    }
    
    private void Save()
    {
        var json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("Stars", json);

        string newJson = json.Replace('"', '|');

        CloudVariables.NewJson = newJson;
        Cloud.Storage.Save();
    }
    
    public int GetStars(int levelId)
    {
        var l = _levelInfo.Find(_ => _.LevelId == levelId);
        if (l == null)
        {
            _levelInfo.Add(new LevelData
            {
                LevelId = levelId,
                LevelStars = 0, 
            });
            Debug.LogWarning("Not found level, give zero stars");
            return 0;
        }

        return l.LevelStars;
    }

    public void UpdateLevelDataAndSave(int levelId, int stars)
    {
        var l = _levelInfo.Find(_ => _.LevelId == levelId);
        if (l == null)
        {
            _levelInfo.Add(new LevelData
            {
                LevelId = levelId,
                LevelStars = stars,
            });
        }
        else
        {
            if (stars > l.LevelStars)
            {
                l.LevelStars = stars;
            }
        }
        
        Save();
        
    }
    
}

[System.Serializable]
public class LevelData
{
    public int LevelId = -1;
    public int LevelStars = -1;
}
