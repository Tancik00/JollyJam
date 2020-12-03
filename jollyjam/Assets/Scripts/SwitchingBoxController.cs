using System;
using System.Collections;
using UnityEngine;

public class SwitchingBoxController : MonoBehaviour
{
    //public Sprite box;
    //public Sprite filledBox;   

    public MainConfigScriptableObj mainConfigScriptableObj;
    
    private Sprite _obstacle;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        var skinID = LevelGeneratorController.GetInstance().level.GetComponent<LevelsScriptData>().skinID;
        //box = mainConfigScriptableObj.skins[skinID].box;
        _obstacle = mainConfigScriptableObj.skins[skinID].changedBox;

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //filledBox = mainConfigScriptableObj.skins[skinID].filledBox;
        StartCoroutine(ChangeBox());
    }

    private void Update()
    {
        if (!AppStateData.IsPause)
        {
            _spriteRenderer.enabled = true;
        }

        if (AppStateData.IsPause)
        {
            _spriteRenderer.enabled = false;
        }
    }

    private void ChangeToBox()
    {
        //if (!gameObject.GetComponent<Checker>().isChecked)
        //{
        _spriteRenderer.sprite = null;
           //_spriteRenderer.sortingOrder = 1;
        //}
        /*else
        {
            _spriteRenderer.sprite = null;
        }*/
        gameObject.layer = LayerMask.NameToLayer("Default");
    } 
    
    private void ChangeToObstacle()
    {
        _spriteRenderer.sprite = _obstacle;
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
    }

    private IEnumerator ChangeBox()
    {
        ChangeToObstacle();
        yield return new WaitForSeconds(1f);
        ChangeToBox();
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeBox());
    }
}
