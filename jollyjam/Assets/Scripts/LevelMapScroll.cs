using CloudOnce;
using UnityEngine;

public class LevelMapScroll : MonoBehaviour
{
    
    private Transform content;
    private Vector3 _startTouch, _currentPos;
    private Vector3 scrollPosition;
    
    private float bottomLimit = 0;
    private float topLimit = 0;
    private int endOfTheFirstZone = 60;
    private int endOfTheSecondZone = 120;
    private int endOfTheThirdZone = 180;
    private int endOfTheFourthZone = 240;

    //public float elasticCoef = 0.94f;
    public float scrollSpeed = 0.1f;
    public float swipeMultiplier=3f;
    public GameObject map;
    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;
    public GameObject zone5;
    public LevelManager levelManager;

    private void Start()
    {
        MovableBallController.count = 0;
        GameTimeController.passedTime = 0;
        SwipeCountPanel.swipeCount = 0;
        content = transform;
        scrollPosition = content.position;

        SetZonesActive();
        
        ScrollMapToCurrentLevel();
        
        SetMaxHeightMarker();
        
        levelManager.Move();

        if (LevelManager.localCurrentLevel < 10)
        {
            var cameraPosition = scrollPosition;
            cameraPosition.y = 0.3f;                     //камера на начальных уровнях съежала вниз(до 10 уровня)
            content.position = cameraPosition;
        }
    }

    private void ScrollMapToCurrentLevel()
    {
        Transform curretLevel;
        levelManager.InitLevels();

        if (LevelManager.localCurrentLevel < 0)
        { 
            curretLevel = levelManager.mas[CloudVariables.LastLevel].transform;
        }
        else
        {
            curretLevel = levelManager.mas[LevelManager.localCurrentLevel].transform;
        }
        //if curretLevel.position.y > camera.sie/2
        
        SetTargetScrollY(curretLevel.position.y);
#if UNITY_EDITOR
                Debug.Log(curretLevel.position.y);
#endif
    }

    private void SetMaxHeightMarker()
    {
        //check map gameobject and find UP bound
        MaxHeightMarker[] markers = map.GetComponentsInChildren<MaxHeightMarker>();
        MaxHeightMarker maxmark = markers[0];
        foreach (var marker in markers)
        {
            if (marker.transform.position.y>maxmark.transform.position.y)
            {
                maxmark = marker;
            }
        }
        topLimit = maxmark.transform.position.y - Camera.main.orthographicSize;
#if UNITY_EDITOR
         Debug.Log("topLimit = "+ topLimit);
#endif
       
    }

    private void SetZonesActive()
    {
        if (CloudVariables.LastLevel >= endOfTheFirstZone)
        {
            zone2.SetActive(true);
        }
        if (CloudVariables.LastLevel >= endOfTheSecondZone)
        {
            zone3.SetActive(true);
        }
        if (CloudVariables.LastLevel >= endOfTheThirdZone)
        {
            zone4.SetActive(true);
        }
        if (CloudVariables.LastLevel >= endOfTheFourthZone)
        {
            zone5.SetActive(true);
        }
    }

    public void SetTargetScrollY(float targetY)
    {
        scrollPosition.y = targetY;
        content.position = scrollPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
        }
        
        Vector3 newPos = content.position;
        newPos.y = Mathf.Lerp(content.position.y,scrollPosition.y,scrollSpeed);
        content.position = newPos;
        
        if (Input.GetMouseButton(0))
        {
            _currentPos = Input.mousePosition;
            var swipeSpeed = (_startTouch.y - _currentPos.y)*Time.deltaTime*swipeMultiplier;
            scrollPosition.y = content.position.y + swipeSpeed;
            _startTouch = Input.mousePosition;
        }
        SpringToEdge();
    }
    
    private void SpringToEdge() {

        if(scrollPosition.y < bottomLimit) {
            scrollPosition.y = bottomLimit;
            return;
        }
        if(scrollPosition.y > topLimit) {
            scrollPosition.y = topLimit;
        }
    }
}
