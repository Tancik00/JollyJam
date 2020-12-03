using System.Collections;
using System.Collections.Generic;
using Config;
using DG.Tweening;
using GameAnalyticsSDK.Setup;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "MainConfigScriptableObj", menuName = "MainConfigScriptableObj", order = 51)]
public class MainConfigScriptableObj : ScriptableObject
{
    public float cameraCoeficient = 0.05f; // Main Config
    public float backgroundSizeCoeficient = 0.07f;
    public GameObject[] levels;
    
    public SkinScriptableObject[] skins;
    private SkinScriptableObject currentSkinConfig;

    [ContextMenu("ApplySkins")]
    public void ApplySkins()
    {
        Debug.Log("ApplySkins");

        for (int i = 0; i < levels.Length; i++)
        {
            var level = levels[i];
            var skinId = level.GetComponent<LevelsScriptData>().skinID;
            currentSkinConfig = skins[skinId];
            var childCount = level.transform.childCount;
            var child1 = level.transform;

            for (int j = 0; j < childCount; j++)
            {
                var child = child1.GetChild(j);
                /*if (child.CompareTag("box"))
                {
                    child.GetComponent<SpriteRenderer>().sprite = currentSkinConfig.box;
                }*/

                if (child.CompareTag("obstacle"))
                {
                    child.GetComponent<SpriteRenderer>().sprite = currentSkinConfig.obstacle;
                }

                if (child.CompareTag("changingBox"))
                {
                    child.GetComponent<SpriteRenderer>().sprite = currentSkinConfig.changingBox;
                }

                if (child.CompareTag("bot"))
                {
                    child.GetComponent<SpriteRenderer>().sprite = currentSkinConfig.bot;
                }
                
                //EditorUtility.SetDirty(child);
                
            }
        }
        //AssetDatabase.SaveAssets();
    }
#if UNITY_EDITOR

    public TextAsset TextAsset;
    public MainConfigScriptableObj SO;
    [ContextMenu("ApplyFromJson")]
    void Apply()
    {
        var json = JsonUtility.FromJson<PrefabData>(TextAsset.text);
        for (int i = 1; i < SO.levels.Length; i++)
        {
            var d = SO.levels[i-1].GetComponent<LevelsScriptData>();
            var d2 = json.Datas.Find(_ => _.__2 == i);
            Debug.Log( $"{i} : {d2.__6} {d.gameObject.name}");
            d.min = d2.__6;
            d.medium = d2.__7;
            d.max = d2.__8;
            EditorUtility.SetDirty(d);
        }
        
        EditorUtility.SetDirty(SO);
        AssetDatabase.SaveAssets();
    }
    
    [System.Serializable]
    public class PrefabData
    {
        public List<Data> Datas;
    }

    [System.Serializable]
    public class Data
    {
        public int __2 =  1;
        public int __3 =  1;
        public int __4 =  0;
        public int __5 =  4;
        public int __6 =  5;
        public int __7 =  7;
        public int __8 =  1;
    }
    
#endif

}
