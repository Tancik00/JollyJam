using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigScriptableObject", menuName = "PlayerConfigScriptableObject", order = 51)]
public class PlayerConfigScriptableObject : ScriptableObject
{
    public float speedMod = 0.1f;
    public float incScale = 1.5f, decScale = 0.8f;
    public Ease animationEase = Ease.Linear;
    public Sprite newSprite;
    public Sprite obstacleSprite;
    public float animationDuration = 0.2f;
}