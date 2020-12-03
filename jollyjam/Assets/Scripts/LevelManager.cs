using System.Collections;
using CloudOnce;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [FormerlySerializedAs("_uiManager")] [SerializeField]
    private GamePlayUIManager gamePlayUiManager;

    public GameObject pref;

    private int _levelNumber = -1;

    public GameObject[] mas;
    public SplineFollower splineFollower;
    public SplineComputer splineComputer;

    public Sprite buttonSprite;
    
    private double currentPercent;
    private double targetPercent;

    private int btnObjNum;
    
    public static int localCurrentLevel = -1;

    private void Start()
    {
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i].transform.rotation=new Quaternion(0,0,0,0);
        }
    }

    public void Move()
    {
        Transform targetLevel;
        if (localCurrentLevel < 0)
        {
            btnObjNum = CloudVariables.LastLevel - 1;
        }
        else
        {
            btnObjNum = localCurrentLevel - 1;
        }

        if (btnObjNum < 0)
        {
            targetLevel = mas[0].transform;
        }
        else
        {
            targetLevel = mas[btnObjNum].transform;
        }

        currentPercent = splineComputer.Project(targetLevel.position);
        targetLevel = mas[btnObjNum + 1].transform;
        targetPercent = splineComputer.Project(targetLevel.position, 5);

        splineFollower.SetPercent(currentPercent);

        if (localCurrentLevel == CloudVariables.LastLevel)
        {
            StartCoroutine(AnimateLevelBtn());
            StartCoroutine(AnimateHeroOnMap());
        }
        else
        {
            splineFollower.SetPercent(targetPercent);
            StartCoroutine(AnimateLevelBtn());
        }
    }

    IEnumerator AnimateHeroOnMap()
    {
        var startProc = (float) currentPercent;
        var endProc = (float) targetPercent;
        float time = 0f;
        while (true)
        {
            yield return null;
            var dist = Mathf.Lerp(startProc, endProc, time);
            splineFollower.SetPercent(dist);
            time += Time.deltaTime;
            if (time >= 1f)
                break;
        }

        currentPercent = splineComputer.Project(splineFollower.transform.position);


        yield return null;
    }

    IEnumerator AnimateLevelBtn()
    {
        Transform lvlBtn;
        if (localCurrentLevel < 0)
        {
            lvlBtn = mas[CloudVariables.LastLevel].transform;
        }
        else
        {
            lvlBtn = mas[localCurrentLevel].transform;
        }

        lvlBtn.GetChild(0).GetComponent<Image>().sprite = buttonSprite;
        DOTween.To(() => lvlBtn.localScale, x => lvlBtn.localScale = x, new Vector3(1.4f, 1.4f, 1.5f), 1f);
        yield return null;
    }
    
    public void InitLevels()
    {
        GameObject obj;
        for (int i = 0; i < mas.Length; i++)
        {
            _levelNumber++;
            obj = Instantiate(pref, mas[i].transform);
            obj.GetComponent<LevelBtnView>()
                .SetUp(gamePlayUiManager, _levelNumber, i <= CloudVariables.LastLevel);
        }
    }
}
