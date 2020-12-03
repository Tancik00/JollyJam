using System;
using UnityEngine;
using Object = System.Object;
#if UNITY_EDITOR
using GameAnalyticsSDK.Setup;
using UnityEditor;
/// <summary>
/// 
/// </summary>
[ExecuteInEditMode] 
public class LevelEditorBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject changingPrefab;
    [SerializeField] private bool isOn = false;
    
    private void OnEnable()
    {
        //Debug.Log("OnEnable");
        if (!Application.isEditor)
        {
            Destroy(this);
        }
        SceneView.onSceneGUIDelegate += OnScene;
    }    
    void OnScene(SceneView scene)
    {
        Event e = Event.current;
 
        return;
        if (e.type == EventType.MouseDown)
        {
            GameObject currentObject = Selection.activeObject as GameObject;
            if (currentObject != null && (currentObject.CompareTag("box") || currentObject.CompareTag("obstacle")||currentObject.CompareTag("changingBox") ))
            {

                if (e.button == 1)
                {
                    ChangeCurrentToPrefab(changingPrefab, currentObject);
                    return;
                }
                if (currentObject.CompareTag("box"))
                {
                    ChangeCurrentToPrefab(obstaclePrefab, currentObject);
                }
                else
                {
                    ChangeCurrentToPrefab(roadPrefab, currentObject);
                }
            }
        }
    }

    private void ChangeCurrentToPrefab(GameObject prefabToChange, GameObject currentObject)
    {
        GameObject newPref = PrefabUtility.InstantiatePrefab(prefabToChange,currentObject.transform.parent)as GameObject;
        newPref.transform.position = currentObject.transform.position;
        DestroyImmediate(currentObject);
    }
}
#endif

