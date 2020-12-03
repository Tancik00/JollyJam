using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupConfig", menuName = "PopupConfig", order = 51)]
public class PopupConfig : ScriptableObject
{
   public GameObject[] popups;
}